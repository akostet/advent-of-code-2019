//using advent_of_code_2019.helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using advent_of_code_2019.helpers;
using Point = System.Drawing.Point;

namespace advent_of_code_2019
{
    public static class Day10
    {
        public static int Problem1(IEnumerable<string> input)
        {
            var map = input.ToList();
            var asteroidLocations = new HashSet<Point>();

            for(var y = 0;  y < map.Count; y++)
            {
                var line = map[y].ToCharArray();
                for(var x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                        asteroidLocations.Add(new Point(x, y));
                }
            }
            var maxReachable = int.MinValue;

            foreach (var asteroid in asteroidLocations)
            {
                //var otherAsteroids = asteroidLocations.Except(new[] { asteroid });
                var otherAsteroids = new HashSet<Point>(asteroidLocations.Except(new[] { asteroid }));

                var reachableCount = 0;
                foreach(var otherAsteroid in otherAsteroids)
                {
                    if (IsReachable(asteroid, otherAsteroid, otherAsteroids))
                        reachableCount++;
                }
                maxReachable = Math.Max(maxReachable, reachableCount);
            }

            return maxReachable;
        }

        private static bool IsReachable(Point p1, Point p2, HashSet<Point> otherPoints)
        {
            var deltaX = Math.Abs(p1.X - p2.X);
            var deltaY = Math.Abs(p1.Y - p2.Y);

            var xDir = p1.X < p2.X ? -1 : 1;
            var yDir = p1.Y < p2.Y ? -1 : 1;

            if(deltaX == 0)
            {
                for(var y = 1; y < deltaY; y++)
                {
                    var p = new Point(p2.X, p2.Y + (y * yDir));
                    if (otherPoints.Contains(p))
                        return false;
                }
                return true;
            }

            if(deltaY == 0)
            {
                for (var x = 1; x < deltaX; x++)
                {
                    var p = new Point(p2.X + (x * xDir), p2.Y);
                    if (otherPoints.Contains(p))
                        return false;
                }
                return true;
            }

            var gcd = MathExtensions.GreatestCommonDivisor(deltaX, deltaY);

            if (gcd == 1)
                return true; 

            deltaX /= gcd;
            deltaY /= gcd;

            var possiblyBlockingX = p2.X + (deltaX*xDir);
            var possiblyBlockingY = p2.Y + (deltaY*yDir);

            while(possiblyBlockingX != p1.X && possiblyBlockingY != p1.Y)
            {
                if (otherPoints.Contains(new Point(possiblyBlockingX, possiblyBlockingY)))
                    return false;

                possiblyBlockingX += deltaX * xDir;
                possiblyBlockingY += deltaY * yDir;
            }

            return true;
        }
    }
}
