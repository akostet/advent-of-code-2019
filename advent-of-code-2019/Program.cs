using advent_of_code_2019.helpers;
using System;
using System.Linq;

namespace advent_of_code_2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var day1Input = InputReader.ReadInput(day: 1).Select(x => int.Parse(x));
            Console.WriteLine($"Day one, problem 1: {Day1.Problem1(day1Input)}");
            Console.WriteLine($"Day one, problem 2: {Day1.Problem2(day1Input)}");
            Console.ReadLine();
        }
    }
}
