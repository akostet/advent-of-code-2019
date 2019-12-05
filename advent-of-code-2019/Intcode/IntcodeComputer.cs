using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace advent_of_code_2019.Intcode
{
    public class IntcodeComputer
    {
        private List<int> _memory;
        private Dictionary<int, IntcodeInstruction> _instructionSet;

        public IntcodeComputer(Dictionary<int, IntcodeInstruction> instructionSet)
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
            for (int pointer = 0; pointer < _memory.Count;)
            {
                var opCode = _memory[pointer];

                if (opCode == 99)
                    break;

                var instruction = _instructionSet[opCode];
                var parameters = new List<int>();

                for (int i = pointer + 1; i <= pointer + instruction.ParametersLength; i++)
                {
                    var parameterLocation = _memory[i];
                    parameters.Add(_memory[parameterLocation]);
                }

                var outputAddress = _memory[pointer + instruction.ParametersLength + 1];

                _memory[outputAddress] = instruction.Operation(parameters.ToArray());

                pointer += instruction.ParametersLength == 0 ? 1 : instruction.ParametersLength + 2;
            }
            return _memory[0];
        }
    }

    public class IntcodeInstruction
    {
        public Func<int[], int> Operation { get; set; }
        public int ParametersLength { get; set; }
    }
}
