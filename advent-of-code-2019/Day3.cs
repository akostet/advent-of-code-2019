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
        
        private static IEnumerable<Point> EvaluatePoints(IEnumerable<string> path1Steps)
        {
            var current = new Point(0, 0);
            var allPoints = new List<Point>();
            foreach (var step in path1Steps)
            {
                var (direction, length) = (step[0], int.Parse(step.Substring(1)));
                var points = Enumerable.Empty<Point>();
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
                }

                allPoints.AddRange(points);
            }

            return allPoints;
        }
    }
}
