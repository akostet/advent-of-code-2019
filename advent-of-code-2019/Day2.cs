using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2019
{
    public static class Day2
    {

        public static int Problem1(int[] input)
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

        public static int[] ProcessOpCodes(int[] input)
        {
            for (int i = 0; i < input.Length; i += 4)
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
