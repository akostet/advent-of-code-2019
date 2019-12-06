using advent_of_code_2019.helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace advent_of_code_2019
{
    class Program
    {
        public static IEnumerable<int> day1Input;
        public static IEnumerable<int> day2Input;
        public static IEnumerable<string> day3Input;
        public static string day4Input;
        public static IEnumerable<int> day5Input;
        public static IEnumerable<string> day6Input;
        static void Main(string[] args)
        {
            GetInputs();

            string input;
            int choice;

            while(true)
            { 
                Console.WriteLine("Enter which day to run (0 to run all):");
                input = Console.ReadLine();
                RunDay(input);
                Console.WriteLine("---------------------------------------");
            } 
        }

        static void RunDay(string day)
        {
            if (day == "0")
            {
                RunAll();
                return;
            }

            //Use reflection to find the implementing class
            var problemClass = Type.GetType($"advent_of_code_2019.Day{day}");

            if(problemClass == null)
            {
                Console.WriteLine($"Day {day} is not yet implemented");
                return;
            }

            var problem1
                 = problemClass.GetMethod("Problem1", BindingFlags.Static | BindingFlags.Public);
            var problem2
                 = problemClass.GetMethod("Problem2", BindingFlags.Static | BindingFlags.Public);

            var input = typeof(Program).GetFields()
                .First(field => field.Name == $"day{day}Input")
                .GetValue(null);

            Console.WriteLine(
                problem1 == null ?
                "Problem 1 not yet implemented" :
                $"Day {day}, problem 1: {problem1.Invoke(null, new object[] { input })}"
            );
            Console.WriteLine(
                problem2 == null ?
                "Problem 2 not yet implemented" :
                $"Day {day}, problem 2: {problem2.Invoke(null, new object[] { input })}"
            );
        }

        static void RunAll()
        {
            Console.WriteLine($"Day 1, problem 1: {Day1.Problem1(day1Input)}");
            Console.WriteLine($"Day 1, problem 2: {Day1.Problem2(day1Input)}");

            Console.WriteLine($"Day 2, problem 1: {Day2.Problem1(day2Input)}");
            Console.WriteLine($"Day 2, problem 2: {Day2.Problem2(day2Input)}");

            Console.WriteLine($"Day 3, problem 1: {Day3.Problem1(day3Input)}");
            Console.WriteLine($"Day 3, problem 2: {Day3.Problem2(day3Input)}");

            Console.WriteLine($"Day 4, problem 1: {Day4.Problem1(day4Input)}");
            Console.WriteLine($"Day 4, problem 2: {Day4.Problem2(day4Input)}");

            Console.WriteLine($"Day 5, problem 1: {Day5.Problem1(day5Input)}");
            Console.WriteLine($"Day 5, problem 2: {Day5.Problem2(day5Input)}");

            Console.WriteLine($"Day 6, problem 1: {Day6.Problem1(day6Input)}");
        }

        static void GetInputs()
        {
            day1Input = InputReader.ReadInput(day: 1).Select(x => int.Parse(x));
            day2Input = InputReader.ReadInput(day: 2, delimiter: ",").Select(x => int.Parse(x)).ToList();
            day3Input = InputReader.ReadInput(day: 3).ToList();
            day4Input = InputReader.ReadInput(day: 4).First();
            day5Input = InputReader.ReadInput(day: 5, delimiter: ",").Select(x => int.Parse(x)).ToList();
            day6Input = InputReader.ReadInput(day: 6).ToList();
        }
    }
}
