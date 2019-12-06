using advent_of_code_2019.helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2019
{
    class Program
    {
        static void Main(string[] args)
        {
            //var day1Input = InputReader.ReadInput(day: 1).Select(x => int.Parse(x));
            //Console.WriteLine($"Day 1, problem 1: {Day1.Problem1(day1Input)}");
            //Console.WriteLine($"Day 1, problem 2: {Day1.Problem2(day1Input)}");

            //var day2Input = InputReader.ReadInput(day: 2, delimiter: ",").Select(x => int.Parse(x)).ToList();
            //Console.WriteLine($"Day 2, problem 1: {Day2.Problem1(day2Input.ToList())}");
            //Console.WriteLine($"Day 2, problem 2: {Day2.Problem2(day2Input.ToList())}");

            //var day3Input = InputReader.ReadInput(day: 3).ToList();
            //Console.WriteLine($"Day 3, problem 1: {Day3.Problem1(day3Input)}");
            //Console.WriteLine($"Day 3, problem 2: {Day3.Problem2(day3Input)}");

            //var day4Input = "248345-746315".Split("-").Select(x => int.Parse(x));
            //Console.WriteLine($"Day 4, problem 1: {Day4.Problem1(day4Input.First(), day4Input.Last())}");
            //Console.WriteLine($"Day 4, problem 2: {Day4.Problem2(day4Input.First(), day4Input.Last())}");

            //var day5Input = InputReader.ReadInput(day: 5, delimiter: ",").Select(x => int.Parse(x)).ToList();
            //Console.WriteLine($"Day 5, problem 1: (enter input 1)");
            //Day5.Problem1(day5Input.ToList());
            //Console.WriteLine($"Day 5, problem 2: (enter input 5)");
            //Day5.Problem2(day5Input);

            var day6Input = InputReader.ReadInput(day: 6).ToList();
            //var day6Input = File.ReadAllLines(@"..\..\..\input\day6example.txt").ToList();


            Console.WriteLine($"Day 6, problem 1: {Day6.Problem1(day6Input)}");

            Console.ReadLine();
        }
    }
}
