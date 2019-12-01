using advent_of_code_2019.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2019
{
    public static class Day1
    {
        public static int Problem1(IEnumerable<int> input)
        {       
            return input.Sum(x => (x / 3) - 2);
        }

        public static int Problem2(IEnumerable<int> input)
        {
            return input.Sum(x => SumOfFuels(x, 0));
        }

        private static int SumOfFuels(int fuel, int sum)
        {
            fuel = (fuel / 3) - 2;

            if (fuel <= 0)
                return sum;

            sum += fuel;

            return SumOfFuels(fuel, sum);
        }
    }
}
