using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace advent_of_code_2019.helpers
{
    public static class InputReader
    {
        public static IEnumerable<string> ReadInput(int day)
        {
            var filePath = @$"..\..\..\input\day{day}.txt";
            return File.ReadAllLines(filePath);            
        }
    }
}
