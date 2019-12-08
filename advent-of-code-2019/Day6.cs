using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using advent_of_code_2019.helpers;

namespace advent_of_code_2019
{
    public static class Day6
    {
        public static int Problem1(IEnumerable<string> input)
        {
            Graph<string> graph = GenerateGraph(input);

            var rootNode = graph.FindByValue("COM");
            int sum = 0;

            CountOrbits(rootNode, 0, ref sum);

            return sum;
        }


        public static int Problem2(IEnumerable<string> input)
        {
            var graph = GenerateGraph(input);
            var initalNode = graph.FindByValue("YOU");
            var targetNode = graph.FindByValue("SAN");

            var distance = DijkstraShortestPath(graph, initalNode, targetNode) - 2; //The algorithm is counting the start and end node, so subtract 2 from the answer

            return distance;
        }
        private static Graph<string> GenerateGraph(IEnumerable<string> input)
        {
            var graph = new Graph<string>();

            foreach (var connection in input)
            {
                var nodes = connection.Split(")");
                graph.AddLink(nodes[1], nodes[0]);
            }

            return graph;
        }


        public static void CountOrbits(Node<string> node, int depth, ref int sum)
        {
            foreach (var childLink in node.IncomingLinks)
            {
                CountOrbits(childLink.ToNode, depth + 1, ref sum);
            }

            sum += depth;
        }

    

        public static int DijkstraShortestPath(Graph<string> graph, Node<string> initialNode, Node<string> targetNode)
        {
            var queue = new Queue<KeyValuePair<Node<string>, int>>();
            queue.Enqueue(new KeyValuePair<Node<string>, int>(initialNode, 0));

            var distance = new Dictionary<Node<string>, int>();

            foreach (var node in graph.Nodes)
                distance[node] = int.MaxValue;

            distance[initialNode] = 0;
            while (queue.Count() > 0)
            {
                var currentNode = queue.Dequeue();

                var allConnections = currentNode.Key.OutgoingLinks;
                allConnections.AddRange(currentNode.Key.IncomingLinks);
                foreach (var connection in allConnections)
                {
                    var neighbour = connection.ToNode;
                    var newDistance = distance[currentNode.Key] + 1;
                    if (newDistance < distance[neighbour])
                    {
                        distance[neighbour] = newDistance;
                        queue.Enqueue(new KeyValuePair<Node<string>, int>(neighbour, newDistance));
                    }
                }
            }

            return distance[targetNode];
        }   
    }
}
