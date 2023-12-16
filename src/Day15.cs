using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    class Day15
    {
        public long Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\input\day15.txt");

            return RoundB(input);
        }

        private long RoundA(string input)
        {
            var input2 = input.Split("\r\n").Select(ParseInput).ToList();
            var row = 2000000;

            var minX = input2[0].s.x;
            var maxX = input2[0].s.x;
            foreach (var line in input2)
            {
                if (line.s.x < minX) {
                    minX = line.s.x;
                }
                if (line.b.x < minX)
                {
                    minX = line.b.x;
                }
                if (maxX < line.s.x)
                {
                    maxX = line.s.x;
                }
                if (maxX < line.b.x)
                {
                    maxX = line.b.x;
                }
            }
            minX -= 5000000;
            maxX += 6000000;

            var cantHaveBeacon = 0;

            for (var i = minX; i <= maxX; i++)
            {
                var possibleCantHaveBeacon = false;
                foreach (var line in input2)
                {
                    if ((i, row) == line.s || (i, row) == line.b)
                    {
                        possibleCantHaveBeacon = false;
                        break;
                    }
                    if (GetDistance(line.s, line.b) >= GetDistance(line.s, (i, row)))
                    {
                        possibleCantHaveBeacon = true;
                    }
                }

                if (possibleCantHaveBeacon)
                {
                    cantHaveBeacon++;
                }
            }


            return cantHaveBeacon;
        }

        private long RoundB(string input)
        {
            var input2 = input.Split("\r\n").Select(ParseInput).ToList();

            var emptyPoint = ((long)0, (long)0);

            foreach (var line in input2)
            {
                var outPoint = NextPointOutCircle(line.s, line.b);
                var initialPoint = NextPointOnCircle(line.s, outPoint);
                while (initialPoint != outPoint)
                {
                    if (input2.All(x => GetDistance(x.s, x.b) < GetDistance(x.s, initialPoint)) &&
                        (0 <= initialPoint.x && initialPoint.x <= 4000000) &&
                        (0 <= initialPoint.y && initialPoint.y <= 4000000))
                    {
                        emptyPoint = initialPoint;
                        break;
                    }
                    initialPoint = NextPointOnCircle(line.s, initialPoint);
                }
            }

            return (emptyPoint.Item1 * 4000000) + emptyPoint.Item2;

        }

        private ((long x, long y) s, (long x, long y) b) ParseInput(string line)
        {
            var result = line.Split(": ").SelectMany(x => x.Split(", ")).Select(x => long.Parse(x.Split("=")[1])).ToArray();
            return ((result[0], result[1]), (result[2], result[3]));
        }

        private long GetDistance((long, long) x, (long, long) y)
        {
            return long.Abs(x.Item1 - y.Item1) + long.Abs(x.Item2 - y.Item2);
        }

        private (long x, long y) NextPointOnCircle((long x, long y) c, (long x, long y) p)
        {
            if (c.x < p.x && c.y <= p.y)
            {
                return (p.x - 1, p.y + 1);
            }
            else if (c.x >= p.x && c.y < p.y)
            {
                return (p.x - 1, p.y - 1);
            }
            else if (c.x > p.x && c.y >= p.y)
            {
                return (p.x + 1, p.y - 1);
            }
            else
            {
                return (p.x + 1, p.y + 1);
            }
        }

        private (long x, long y) NextPointOutCircle((long x, long y) c, (long x, long y) p)
        {
            if (c.y <= p.y)
            {
                return (p.x, p.y + 1);
            }
            else
            {
                return (p.x, p.y - 1);
            }
        }
    }
}
