using advent_of_code_2019.Intcode;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace advent_of_code_2019
{
    public static class Day2
    {

        public static int Problem1(List<int> input)
        {
            /*
             * Once you have a working computer, the first step is to restore the gravity assist program (your puzzle input) to the "1202 program alarm" state it had just before the last computer caught fire.
             * To do this, before running the program, replace position 1 with the value 12 and replace position 2 with the value 2. What value is left at position 0 after the program halts?
             */

            input[1] = 12;
            input[2] = 2;

            input = ProcessOpCodes(input);

            return input[0];
        }

        public static int Problem2(List<int> input)
        {
            var instructionSet = new Dictionary<int, IntcodeInstruction>()
            {
                { 1, new IntcodeInstruction(){ ParametersLength = 2, Operation = parameters => parameters.Sum() } },
                { 2, new IntcodeInstruction(){ ParametersLength = 2, Operation = parameters => parameters.Aggregate( (result, item) => result * item ) } },
                { 99, new IntcodeInstruction(){ ParametersLength = 0 } }
            };

            for(int noun = 0; noun < 100; noun++)
            {
                for(int verb = 0; verb < 100; verb++)
                {
                    var inputCopy = input.ToList();
                    var computer = new IntcodeComputer(inputCopy, instructionSet, noun, verb);
                    var result = computer.Evaluate();
                    var output = result[0];

                    if (output == 19690720)
                        return (100 * noun) + verb;
                }
            }

           
            return -1;
        }

        public static List<int> ProcessOpCodes(List<int> input)
        {
            for (int i = 0; i < input.Count; i += 4)
            {
                var opCode = input[i];

                if (opCode == 99)
                    break;

                var n1Address = input[i + 1];
                var n2Address = input[i + 2];
                var oAddress = input[i + 3];
                var n1 = input[n1Address];
                var n2 = input[n2Address];

                input[oAddress] = opCode == 1 ? n1 + n2 : n1 * n2;
            }

            return input;
        }
    }
}
