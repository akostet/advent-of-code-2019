using System;
using System.Collections.Generic;

namespace advent_of_code_2019.Intcode
{
    public class IntCodeComputer
    {
        private readonly int OpCodeLength = 2;
        private List<int> _memory;
        private readonly Dictionary<int, IntCodeInstruction> _instructionSet;

        public IntCodeComputer(Dictionary<int, IntCodeInstruction> instructionSet)
        {
            _instructionSet = instructionSet;
        }

        public void LoadMemory(List<int> memory)
        {
            _memory = memory;
        }

        public void SetState(int noun, int verb)
        {
            _memory[1] = noun;
            _memory[2] = verb;
        }

        public int Evaluate()
        {
            for (var pointer = 0; pointer < _memory.Count;)
            {
                var unparsedOpCode = _memory[pointer];

                var instruction = InterpretInstruction(unparsedOpCode);

                if (instruction.OpCode == 99)
                    break;

                var parameters = new List<int>();
                var parameterModes = GetParameterModes(unparsedOpCode, instruction.ParametersLength);

                for (var i = 0; i < instruction.ParametersLength; i++)
                {
                    var parameter = ResolveParameter(_memory[pointer + i + 1], parameterModes[i]);
                    parameters.Add(parameter);
                }
                var outputAddress = _memory[pointer + instruction.ParametersLength + 1];

                instruction.Execute(_memory, parameters.ToArray(), outputAddress);

                pointer += instruction.InstructionType == IntCodeInstruction.IntCodeInstructionType.FunctionWithoutOutput ?
                    instruction.ParametersLength + 1 : //Parameter length + 1 for instruction
                    instruction.ParametersLength + 2; //Parameter length + 1 for instruction + 1 for output
            }
            return _memory[0];
        }

        public IntCodeInstruction InterpretInstruction(int unparsedOpCode)
        {
            var opCode = (int)(unparsedOpCode % Math.Pow(10, OpCodeLength));
            return _instructionSet[opCode];
        }

        public int[] GetParameterModes(int unparsedOpCode, int parametersLength)
        {
            var result = new List<int>();
            unparsedOpCode /= 100; //Remove the instruction part
            for (var i = 0; i < parametersLength; i++)
            {
                var mode = unparsedOpCode % 10;
                result.Add(mode);
                unparsedOpCode /= 10;
            }

            return result.ToArray();
        }

        public int ResolveParameter(int val, int parameterMode)
        {
            return parameterMode == 1
                ? val
                : _memory[val];
        }
    }

    public class IntCodeInstruction
    {
        public int OpCode { get; set; }
        public Func<int[], int> Function { get; set; }
        public IntCodeInstructionType InstructionType { get; set; }
        public int ParametersLength { get; set; }

        public void Execute(List<int> memory, int[] parameters, int outputAddress)
        {
            if (InstructionType == IntCodeInstructionType.FunctionWithoutOutput)
            {
                Function.Invoke(parameters);
                return;
            }

            if (InstructionType == IntCodeInstructionType.FunctionWithOutput)
            {
                var output = Function(parameters);
                memory[outputAddress] = output;
            }

        }

        public enum IntCodeInstructionType
        {
            FunctionWithOutput,
            FunctionWithoutOutput,
            ConditionalInstruction
        }
    }
}
