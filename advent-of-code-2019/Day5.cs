using System;
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
            var computer = GetComputer();
            computer.LoadMemory(input);
            var result = computer.Evaluate();

            return result;
        }

        public static IntCodeComputer GetComputer()
        {
            var instructionSet = new Dictionary<int, IntCodeInstruction>()
            {
                { 1, new IntCodeInstruction(){ OpCode = 1, InstructionType = IntCodeInstruction.IntCodeInstructionType.FunctionWithOutput, ParametersLength = 2, Function = parameters => parameters.Sum() } },
                { 2, new IntCodeInstruction(){ OpCode = 2, InstructionType = IntCodeInstruction.IntCodeInstructionType.FunctionWithOutput, ParametersLength = 2, Function = parameters => parameters.Aggregate( (result, item) => result * item ) } },
                { 3, new IntCodeInstruction(){ OpCode = 3, InstructionType = IntCodeInstruction.IntCodeInstructionType.FunctionWithOutput, ParametersLength = 0, Function = _ =>
                    {
                        Console.WriteLine("Input:");
                        var input = Console.ReadLine();
                        return int.Parse(input);
                    }
                }},
                { 4, new IntCodeInstruction(){ OpCode = 4, InstructionType = IntCodeInstruction.IntCodeInstructionType.FunctionWithoutOutput, ParametersLength = 1, Function = parameters =>
                    {
                        Console.WriteLine(parameters[0]);
                        return int.MinValue;
                    }
                } },
                { 99, new IntCodeInstruction(){ OpCode = 99, InstructionType = IntCodeInstruction.IntCodeInstructionType.FunctionWithoutOutput, ParametersLength = 0 } }
            };

            return new IntCodeComputer(instructionSet);
        }
    }
}
