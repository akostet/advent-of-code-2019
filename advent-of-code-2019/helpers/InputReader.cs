using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace advent_of_code_2019.helpers
{
    public static class InputReader
    {
        public static IEnumerable<string> ReadMultilineInput(int day)
        {
            var filePath = $@"..\..\..\input\day{day}.txt";
            return File.ReadAllLines(filePath);            
        }

        public static IEnumerable<string> ReadInput(int day, string delimiter)
        {
            var filePath = $@"..\..\..\input\day{day}.txt";
            var content = File.ReadAllText(filePath);
            return content.Split(delimiter);
        }
        public static string ReadSingleLineInput(int day)
        {
            var filePath = $@"..\..\..\input\day{day}.txt";
            var content = File.ReadAllText(filePath);
            return content;
        }
    }
}
