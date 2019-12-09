using advent_of_code_2019.Intcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2019
{
    public static class Day9
    {
        static Queue<long> ProgramInput;
        static Queue<long> ProgramOutput;

        public static int Problem1(IEnumerable<long> input)
        {
            ProgramOutput = new Queue<long>();
            ProgramInput = new Queue<long>();

            ProgramInput.Enqueue(1);

            var computer = new IntCodeComputer(GetInstructionSet());
            computer.LoadMemory(input.ToList());

            var executionResult = ExecutionState.Interrupted;
            while(executionResult != ExecutionState.Finished)
            {
                executionResult = computer.Evaluate();
            }

            
            return 3;
        }
        private static List<IntCodeInstruction> GetInstructionSet()
        {
            return new List<IntCodeInstruction>()
            {
                new IntCodeInstruction()
                {
                    OpCode = 1,
                    ParametersLength = 2,
                    Function = (parameters, _) => parameters.Sum()
                },
                new IntCodeInstruction()
                {
                    OpCode = 2,
                    ParametersLength = 2,
                    Function = (parameters, _) => parameters.Aggregate( (result, item) => result * item )
                },
                new IntCodeInstruction()
                {
                    OpCode = 3,
                    ParametersLength = 0,
                    Function = (parameters, _) =>
                    {
                        return ProgramInput.Dequeue();
                    }
                },
                new IntCodeInstruction()
                {
                    OpCode = 4,
                    ParametersLength = 1,
                    Sub = (parameters, _) =>
                    {
                        ProgramOutput.Enqueue(parameters[0]);
                    }
                }, 
                //jump-if-true
                new IntCodeInstruction()
                {
                    OpCode = 5,
                    ParametersLength = 2,
                    Sub = (parameters, executionContext) =>
                    {
                        if (parameters[0] != 0)
                            executionContext.ProgramCounter = (int)parameters[1];
                    }
                },
                //jump-if-false
                new IntCodeInstruction()
                {
                    OpCode = 6,
                    ParametersLength = 2,
                    Sub = (parameters, executionContext) =>
                    {
                        if (parameters[0] == 0)
                            executionContext.ProgramCounter = (int)parameters[1];
                    }
                },
                //less than
                new IntCodeInstruction()
                {
                    OpCode = 7,
                    ParametersLength = 2,
                    Function = (parameters, _) =>
                    {
                        return parameters[0] < parameters[1] ? 1 : 0;
                    }
                },
                //equals
                new IntCodeInstruction()
                {
                    OpCode = 8,
                    ParametersLength = 2,
                    Function = (parameters, _) =>
                    {
                        return parameters[0] == parameters[1] ? 1 : 0;
                    }
                },
                //relative base offset
                new IntCodeInstruction()
                {
                    OpCode = 9,
                    ParametersLength = 1,
                    Sub = (parameters, executionContext) =>
                    {
                        executionContext.RealativeBase += (int)parameters[0];
                    }
                },
                new IntCodeInstruction()
                {
                    OpCode = 99,
                    ParametersLength = 0
                },
            };
        }

    }
}
