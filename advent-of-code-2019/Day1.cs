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
            return input.Sum(CalculateFuelForMass);
        }

        public static int Problem2(IEnumerable<int> input)
        {
            return input.Sum(x => SumOfFuels(x, 0));
        }

        public static int SumOfFuels(int mass, int sum)
        {
            mass = CalculateFuelForMass(mass);

            if (mass <= 0)
                return sum;

            sum += mass;

            return SumOfFuels(mass, sum);
        }

        public static int CalculateFuelForMass(int mass)
        {
            return (mass / 3) - 2;
        }
    }
}
