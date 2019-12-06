using System;
using System.Collections.Generic;
using System.Text;
using advent_of_code_2019.helpers;

namespace advent_of_code_2019
{
    public static class Day6
    {
        public static int Problem1(IEnumerable<string> input)
        {
            Graph<string> graph = new Graph<string>();

            foreach (var connection in input)
            {
                var nodes = connection.Split(")");
                graph.AddLink(nodes[1], nodes[0]);
            }

            var rootNode = graph.FindByValue("COM");
            int sum = 0;

            CountOrbits(rootNode, 0, ref sum);

            return sum;
        }

        public static void CountOrbits(Node<string> node, int depth, ref int sum)
        {
            foreach (var childLink in node.IncomingLinks)
            {
                CountOrbits(childLink.ToNode, depth + 1, ref sum);
            }

            sum += depth;
        }
    }
}
