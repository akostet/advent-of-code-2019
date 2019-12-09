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

        public void LoadMemory(List<long> memory)
        {
            _executionContext = new ExecutionContext(memory);
        }

        public void SetState(int noun, int verb)
        {
            _executionContext.SetMemory(1, noun);
            _executionContext.SetMemory(2, verb);
        }

        public long ReadMemory(int address)
        {
            return _executionContext.ReadMemory(address);
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

                var parameters = new List<long>();
                var parameterModes = GetParameterModes(unparsedOpCode, instruction.ParametersLength);

                for (var i = 0; i < instruction.ParametersLength; i++)
                {
                    //var parameter = ResolveParameter(_memory[pointer + i + 1], parameterModes[i]);
                    var parameter = ResolveParameter(_executionContext.ReadMemory(_executionContext.ProgramCounter + i + 1), parameterModes[i]);
                    parameters.Add(parameter);
                }
                var outputAddress = (int)_executionContext.ReadMemory(_executionContext.ProgramCounter + instruction.ParametersLength + 1);

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

        public long ResolveParameter(long val, int parameterMode)
        {
            return parameterMode == 1
                ? val 
                : parameterMode == 2
                ? _executionContext.ReadMemory(_executionContext.RealativeBase + (int)val)
                : _executionContext.ReadMemory((int)val);
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
        public Func<long[], ExecutionContext, long> Function { get; set; }
        public Action<long[], ExecutionContext> Sub { get; set; }
        public int ParametersLength { get; set; }
        public bool HasReturnValue => Function != null;

        public void Execute(ExecutionContext executionContext, long[] parameters, int outputAddress)
        {
            if (Function != null)
            {
                var output = Function(parameters, executionContext);
                executionContext.SetMemory(outputAddress, output);
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
        public int RealativeBase { get; set; }
        private Dictionary<int, long> Memory { get; set; }
        public bool Interrupted { get; set; }

        public ExecutionContext(List<long> memory)
        {
            Memory = new Dictionary<int, long>();
            for (int idx = 0; idx < memory.Count(); idx++)
                Memory.Add(idx, memory[idx]);
            ProgramCounter = 0;
        }

        public long ReadMemory(int address)
        {
            if (!Memory.ContainsKey(address))
                Memory.Add(address, 0);            

            return Memory[address];
        }

        public void SetMemory(int address, long value)
        {
            Memory[address] = value;
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
            return (int)Memory[ProgramCounter];
        }
    }
}
