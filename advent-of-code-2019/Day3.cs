using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using advent_of_code_2019.helpers;

namespace advent_of_code_2019
{
    public class Day3
    {
        public static int Problem1(List<string> input)
        {
            var path1Steps = input[0].Split(",");
            var path2Steps = input[1].Split(",");

            var path1Points = EvaluatePoints(path1Steps);
            var path2Points = EvaluatePoints(path2Steps);

            var closestIntersection = path1Points
                .Intersect(path2Points)
                .Min(point => Math.Abs(point.X) + Math.Abs(point.Y));

            return closestIntersection;
        }

        public static int Problem2(List<string> input)
        {
            var path1Steps = input[0].Split(",");
            var path2Steps = input[1].Split(",");

            var path1Points = EvaluatePointsWithSteps(path1Steps);
            var path2Points = EvaluatePointsWithSteps(path2Steps);

            var intersectingPoints = path1Points.Keys.Intersect(path2Points.Keys);

            foreach (var intersectingPoint in intersectingPoints)
            {
                var path1NumSteps = path1Points[intersectingPoint];
                var path2NumSteps = path2Points[intersectingPoint];
            }
            var minSteps = intersectingPoints.Min(point => path1Points[point] + path2Points[point]);
            return minSteps;
        }

        private static IEnumerable<Point> EvaluatePoints(IEnumerable<string> steps)
        {
            var current = new Point(0, 0);
            var allPoints = new List<Point>();
            foreach (var step in steps)
            {
                var (direction, length) = (step[0], int.Parse(step.Substring(1)));
                var points = GeneratePoints(direction, current, length);

                allPoints.AddRange(points);
            }

            return allPoints;
        }

        private static IEnumerable<Point> GeneratePoints(char direction, Point current, int length)
        {
            IEnumerable<Point> points;
            switch (direction)
            {
                case 'R':
                    points = Enumerable.Range(current.X + 1, length).Select(x => new Point(x, current.Y));
                    current.X += length;
                    break;
                case 'L':
                    points = Enumerable.Range((current.X - length), length).Select(x => new Point(x, current.Y));
                    current.X -= length;
                    break;
                case 'U':
                    points = Enumerable.Range(current.Y + 1, length).Select(y => new Point(current.X, y));
                    current.Y += length;
                    break;
                case 'D':
                    points = Enumerable.Range((current.Y - length), length).Select(y => new Point(current.X, y));
                    current.Y -= length;
                    break;
                default:
                    throw new ArgumentException("Direction is invalid");
            }

            return points;
        }

        private static Dictionary<Point, int> EvaluatePointsWithSteps(IEnumerable<string> steps)
        {
            var numSteps = 0;
            var allPoints = new Dictionary<Point, int>();
            var current = new Point(0, 0);

            foreach (var step in steps)
            {
                var (direction, length) = (step[0], int.Parse(step.Substring(1)));
                var points = GeneratePoints(direction, current, length);

                foreach(var point in points)
                    if(!allPoints.ContainsKey(point))
                        allPoints.Add(point, ++numSteps);
            }

            return allPoints;
        }
    }
}
