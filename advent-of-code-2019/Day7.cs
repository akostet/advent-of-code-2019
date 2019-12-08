using advent_of_code_2019.helpers;
using advent_of_code_2019.Intcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2019
{
    public static class Day7
    {
        static Queue<int> ProgramInput;
        static Queue<int> ProgramOutput;


        public static int Problem1(IEnumerable<int> input)
        {
            var possibleCombinations = Permutations.GetPermutations("01234");
            var instructionSet = GetInstructionSet();
            var computer = new IntCodeComputer(instructionSet);

            var bestResult = int.MinValue;

            foreach(var combination in possibleCombinations)
            {
                ProgramOutput = new Queue<int>();
                ProgramInput = new Queue<int>();
                ProgramOutput.Enqueue(0);
                for (int i = 0; i < 5; i++)
                {
                    ProgramInput.Enqueue(int.Parse(combination[i].ToString()));
                    var output = ProgramOutput.Dequeue();
                    ProgramInput.Enqueue(output);

                    computer.LoadMemory(input.ToList());
                    computer.Evaluate();
                }

                bestResult = Math.Max(ProgramOutput.Dequeue(), bestResult);
            }

           

            return bestResult;
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
                            executionContext.ProgramCounter = parameters[1];
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
                            executionContext.ProgramCounter = parameters[1];
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
                new IntCodeInstruction()
                {
                    OpCode = 99,
                    ParametersLength = 0
                },
            };
        }
    }
}
