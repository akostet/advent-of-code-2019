﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using advent_of_code_2019.Intcode;

namespace advent_of_code_2019
{
    public static class Day5
    {
        public static int Problem1(List<int> input)
        {
            var computer = GetComputerProblem1();
            computer.LoadMemory(input);
            var result = computer.Evaluate();

            return result;
        }

        public static int Problem2(List<int> input)
        {
            var computer = GetComputerProblem2();
            computer.LoadMemory(input);
            var result = computer.Evaluate();

            return result;
        }

        private static List<IntCodeInstruction> BaseInstructionSet()
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
                        Console.WriteLine("Input:");
                        var input = Console.ReadLine();
                        return int.Parse(input);
                    }
                },
                new IntCodeInstruction()
                {
                    OpCode = 4,
                    ParametersLength = 1,
                    Sub = (parameters, _) =>
                    {
                        Console.WriteLine(parameters[0]);
                    }
                },
                new IntCodeInstruction()
                {
                    OpCode = 99,
                    ParametersLength = 0
                }

            };
        }

        public static IntCodeComputer GetComputerProblem1()
        {
            var instructionSet = BaseInstructionSet();
            return new IntCodeComputer(instructionSet);
        }

        public static IntCodeComputer GetComputerProblem2()
        {
            var instructionSet = BaseInstructionSet();
            var extendedInstructionSet = new List<IntCodeInstruction>()
            {
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
                }

            };
            instructionSet.AddRange(extendedInstructionSet);

            return new IntCodeComputer(instructionSet);
        }
    }
}
