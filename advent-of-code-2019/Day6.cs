using System;
using System.Collections.Generic;
using System.Text;
using advent_of_code_2019.helpers;

namespace advent_of_code_2019
{
    public static class Day6
    {
        public static int Problem1(List<string> input)
        {
            Graph<string> graph = new Graph<string>();

            foreach (var connection in input)
            {
                var nodes = connection.Split(")");
                graph.AddLink(nodes[1], nodes[0]);
            }

            var rootNode = graph.FindByValue("COM");

            var sum = CountOrbits(rootNode, 0, 0);

            return sum;
        }

        public static int CountOrbits(Node<string> node, int depth, int sum)
        {
            foreach (var childLink in node.IncomingLinks)
            {
                sum = CountOrbits(childLink.ToNode, depth + 1, sum);
            }

            return sum + depth;
        }
    }
}
