using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2019.helpers
{
    public static class Permutations
    {
        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            var temp = a;
            a = b;
            b = temp;
        }

        public static List<string> GetPermutations(string combinations)
        {
            int x = combinations.Length - 1;
            List<string> results = new List<string>();
            GetPermutations(combinations.ToCharArray(), 0, x, results);
            return results;
        }

        private static void GetPermutations(char[] list, int recursionDepth, int maxDepth, List<string> results)
        {
            if (recursionDepth == maxDepth)
            {
                results.Add(new string(list));
            }
            else
                for (int i = recursionDepth; i <= maxDepth; i++)
                {
                    Swap(ref list[recursionDepth], ref list[i]);
                    GetPermutations(list, recursionDepth + 1, maxDepth, results);
                    Swap(ref list[recursionDepth], ref list[i]);
                }
        }

    }
}
