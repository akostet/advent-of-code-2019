using advent_of_code_2019.helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static IEnumerable<int> day7Input;
        static void Main()
        {
            GetInputs();

            while(true)
            { 
                Console.WriteLine("Enter which day to run (0 to run all):");
                var input = Console.ReadLine();
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

            var problemTimer = Stopwatch.StartNew();

            PrintHeader();

            if (problem1 == null)
                Console.WriteLine("Problem 1 not yet implemented");
            else
                PrintOutput($"Day {day}, problem 1", problem1.Invoke(null, new object[] { input }), problemTimer);

            problemTimer.Restart();

            if (problem2 == null)
                Console.WriteLine("Problem 2 not yet implemented");
            else
                PrintOutput($"Day {day}, problem 2", problem2.Invoke(null, new object[] { input }), problemTimer);       
        }

        static void RunAll()
        {
            var timerAll = Stopwatch.StartNew();

            var problemTimer = Stopwatch.StartNew();

            PrintHeader();

            PrintOutput("Day 1, problem 1", Day1.Problem1(day1Input), problemTimer);
            problemTimer.Restart();
            PrintOutput("Day 1, problem 2", Day1.Problem2(day1Input), problemTimer);
            problemTimer.Restart();

            PrintOutput("Day 2, problem 1", Day2.Problem1(day2Input), problemTimer);
            problemTimer.Restart();
            PrintOutput("Day 2, problem 2", Day2.Problem2(day2Input), problemTimer);
            problemTimer.Restart();

            PrintOutput("Day 3, problem 1", Day3.Problem1(day3Input), problemTimer);
            problemTimer.Restart();
            PrintOutput("Day 3, problem 2", Day3.Problem2(day3Input), problemTimer);
            problemTimer.Restart();

            PrintOutput("Day 4, problem 1", Day4.Problem1(day4Input), problemTimer);
            problemTimer.Restart();
            PrintOutput("Day 4, problem 2", Day4.Problem2(day4Input), problemTimer);
            problemTimer.Restart();

            PrintOutput("Day 5, problem 1", Day5.Problem1(day5Input), problemTimer);
            problemTimer.Restart();
            PrintOutput("Day 5, problem 2", Day5.Problem2(day5Input), problemTimer);
            problemTimer.Restart();

            PrintOutput("Day 6, problem 1", Day6.Problem1(day6Input), problemTimer);
            problemTimer.Restart();
            PrintOutput("Day 6, problem 2", Day6.Problem2(day6Input), problemTimer);
            problemTimer.Restart();

            PrintOutput("Day 7, problem 1", Day7.Problem1(day7Input), problemTimer);
            problemTimer.Restart(); 
            //PrintOutput("Day 7, problem 2", Day7.Problem2(day7Input), problemTimer.ElapsedMilliseconds);
            //problemTimer.Restart();


            timerAll.Stop();
            Console.WriteLine($"Total elapsed time: {timerAll.ElapsedMilliseconds}ms");
        }

        private static void PrintHeader()
        {
            Console.WriteLine("{0,17} {1,10} {2,10}",
                "Day, problem",
                "Output",
                "Elapsed");
        }

        private static void PrintOutput(string day, object output, Stopwatch sw)
        {
            var elapsedTicks = sw.ElapsedTicks;
            var elapsedTime = ((double)elapsedTicks / (double)TimeSpan.TicksPerMillisecond);
            Console.WriteLine("{0,17} {1,10} {2,10}",
               day,
               output,
               $"{elapsedTime} ms");
        }

        static void GetInputs()
        {
            day1Input = InputReader.ReadInput(day: 1).Select(x => int.Parse(x));
            day2Input = InputReader.ReadInput(day: 2, delimiter: ",").Select(x => int.Parse(x)).ToList();
            day3Input = InputReader.ReadInput(day: 3).ToList();
            day4Input = InputReader.ReadInput(day: 4).First();
            day5Input = InputReader.ReadInput(day: 5, delimiter: ",").Select(x => int.Parse(x)).ToList();
            day6Input = InputReader.ReadInput(day: 6).ToList();
            day7Input = InputReader.ReadInput(day: 7, delimiter: ",").Select(x => int.Parse(x)).ToList();
        }
    }
}
