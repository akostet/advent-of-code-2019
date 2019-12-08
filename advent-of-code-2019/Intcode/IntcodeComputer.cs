using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2019.Intcode
{
    public class IntCodeComputer
    {
        private readonly int OpCodeLength = 2;
        private readonly List<IntCodeInstruction> _instructionSet;
        private ExecutionContext _executionContext;

        public IntCodeComputer(List<IntCodeInstruction> instructionSet)
        {
            _instructionSet = instructionSet;
        }

        public void LoadMemory(List<int> memory)
        {
            _executionContext = new ExecutionContext(memory);
        }

        public void SetState(int noun, int verb)
        {
            _executionContext.Memory[1] = noun;
            _executionContext.Memory[2] = verb;
        }

        public int ReadMemory(int address)
        {
            return _executionContext.Memory[address];
        }

        public ExecutionState Evaluate()
        {
            int steps;
            do
            {
                _executionContext.Interrupted = false;

                var unparsedOpCode = _executionContext.CurrentInstruction();

                var instruction = InterpretInstruction(unparsedOpCode);

                if (instruction.OpCode == 99)
                    return ExecutionState.Finished;

                var parameters = new List<int>();
                var parameterModes = GetParameterModes(unparsedOpCode, instruction.ParametersLength);

                for (var i = 0; i < instruction.ParametersLength; i++)
                {
                    //var parameter = ResolveParameter(_memory[pointer + i + 1], parameterModes[i]);
                    var parameter = ResolveParameter(_executionContext.Memory[_executionContext.ProgramCounter + i + 1], parameterModes[i]);
                    parameters.Add(parameter);
                }
                var outputAddress = _executionContext.Memory[_executionContext.ProgramCounter + instruction.ParametersLength + 1];

                var currentPC = _executionContext.ProgramCounter;

                instruction.Execute(_executionContext, parameters.ToArray(), outputAddress);

                steps = currentPC != _executionContext.ProgramCounter ? 
                    0 :
                    instruction.HasReturnValue
                    ? instruction.ParametersLength + 2 : //Parameter length + 1 for instruction + 1 for output
                    instruction.ParametersLength + 1; //Parameter length + 1 for instruction
            } while (_executionContext.StepProgramCounter(steps) && !_executionContext.Interrupted);

    
            return _executionContext.IsFinished() ? ExecutionState.Finished : ExecutionState.Interrupted;
        }

        public IntCodeInstruction InterpretInstruction(int unparsedOpCode)
        {
            var opCode = (int)(unparsedOpCode % Math.Pow(10, OpCodeLength));
            return _instructionSet.First(instruction => instruction.OpCode == opCode);
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
                : _executionContext.Memory[val];
        }
    }

    public enum ExecutionState
    {
        Interrupted,
        Finished
    }

    public class IntCodeInstruction
    {
        public int OpCode { get; set; }
        public Func<int[], ExecutionContext, int> Function { get; set; }
        public Action<int[], ExecutionContext> Sub { get; set; }
        public int ParametersLength { get; set; }
        public bool HasReturnValue => Function != null;

        public void Execute(ExecutionContext executionContext, int[] parameters, int outputAddress)
        {
            if (Function != null)
            {
                var output = Function(parameters, executionContext);
                executionContext.Memory[outputAddress] = output;
            }

            if (Sub != null)
            {
                Sub.Invoke(parameters, executionContext);
            }
        }

    }

    public class ExecutionContext
    {
        public int ProgramCounter { get; set; }
        public List<int> Memory { get; set; }
        public bool Interrupted { get; set; }

        public ExecutionContext(List<int> memory)
        {
            Memory = memory;
            ProgramCounter = 0;
        }

        public bool StepProgramCounter(int steps)
        {
            ProgramCounter += steps;
            return ProgramCounter < Memory.Count;
        }

        public bool IsFinished()
        {
            return ProgramCounter >= Memory.Count;
        }

        public int CurrentInstruction()
        {
            return Memory[ProgramCounter];
        }
    }
}
