using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2022.Days
{
    class Day18
    {
        public long Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\Input\Day18.txt");

            return RoundB(input);
        }

        private long RoundA(string input)
        {
            var input2 = input.Split("\r\n").Select(ParseInput).ToDictionary(x => x);

            var freeSurfaces = 0;

            foreach (var cube in input2)
            {
                var checkPositions = new(int, int, int)[] {
                    (cube.Key.Item1 + 1, cube.Key.Item2, cube.Key.Item3),
                    (cube.Key.Item1, cube.Key.Item2 + 1, cube.Key.Item3),
                    (cube.Key.Item1, cube.Key.Item2, cube.Key.Item3 + 1),
                    (cube.Key.Item1 - 1, cube.Key.Item2, cube.Key.Item3),
                    (cube.Key.Item1, cube.Key.Item2 - 1, cube.Key.Item3),
                    (cube.Key.Item1, cube.Key.Item2, cube.Key.Item3 - 1),
                };

                foreach (var checkPosition in checkPositions)
                {
                    if (!input2.ContainsKey(checkPosition))
                    {
                        freeSurfaces++;
                    }
                }

            }


            return freeSurfaces;
        }

        private long RoundB(string input)
        {
            var input2 = input.Split("\r\n").Select(ParseInput).ToHashSet();

            var freeSurfaces = 0;

            var first = input2.First();
            (int minX, int minY, int minZ, int maxX, int maxY, int maxZ) edge = (first.Item1, first.Item2, first.Item3, first.Item1, first.Item2, first.Item3);

            var innerSpace = new HashSet<(int, int, int)>();

            foreach (var cube in input2)
            {
                if (cube.Item1 < edge.minX)
                {
                    edge.minX = cube.Item1;
                }
                if (cube.Item2 < edge.minY)
                {
                    edge.minY = cube.Item2;
                }
                if (cube.Item3 < edge.minZ)
                {
                    edge.minZ = cube.Item3;
                }
                if (edge.maxX < cube.Item1)
                {
                    edge.maxX = cube.Item1;
                }
                if (edge.maxY < cube.Item2)
                {
                    edge.maxY = cube.Item2;
                }
                if (edge.maxZ < cube.Item3)
                {
                    edge.maxZ = cube.Item3;
                }
            }

            foreach (var cube in input2)
            {
                var checkPositions = new (int, int, int)[] {
                    (cube.Item1 + 1, cube.Item2, cube.Item3),
                    (cube.Item1, cube.Item2 + 1, cube.Item3),
                    (cube.Item1, cube.Item2, cube.Item3 + 1),
                    (cube.Item1 - 1, cube.Item2, cube.Item3),
                    (cube.Item1, cube.Item2 - 1, cube.Item3),
                    (cube.Item1, cube.Item2, cube.Item3 - 1),
                };

                foreach (var checkPosition in checkPositions)
                {
                    if (!input2.Contains(checkPosition) && !innerSpace.Contains(checkPosition))
                    {
                        var stack = new Stack<(int, int, int)>();
                        stack.Push(checkPosition);
                        var isOutside = false;
                        var insideSpace = new HashSet<(int, int, int)>();
                        insideSpace.Add(checkPosition);
                        while ((stack.Count() != 0) && !isOutside)
                        {
                            var insidePosition = stack.Pop();
                            var checkInsidePositions = new (int, int, int)[] {
                                (insidePosition.Item1 + 1, insidePosition.Item2, insidePosition.Item3),
                                (insidePosition.Item1, insidePosition.Item2 + 1, insidePosition.Item3),
                                (insidePosition.Item1, insidePosition.Item2, insidePosition.Item3 + 1),
                                (insidePosition.Item1 - 1, insidePosition.Item2, insidePosition.Item3),
                                (insidePosition.Item1, insidePosition.Item2 - 1, insidePosition.Item3),
                                (insidePosition.Item1, insidePosition.Item2, insidePosition.Item3 - 1),
                            };

                            foreach (var checkInsidePosition in checkInsidePositions)
                            {
                                if (insidePosition.Item1 < edge.minX || edge.maxX < insidePosition.Item1 ||
                                    insidePosition.Item2 < edge.minY || edge.maxY < insidePosition.Item2 ||
                                    insidePosition.Item3 < edge.minZ || edge.maxZ < insidePosition.Item3)
                                {
                                    isOutside = true;
                                    break;
                                }

                                if (!input2.Contains(checkInsidePosition) &&
                                    !insideSpace.Contains(checkInsidePosition))
                                {
                                    insideSpace.Add(checkInsidePosition);
                                    stack.Push(checkInsidePosition);
                                }
                            }
                        }

                        if (isOutside)
                        {
                            freeSurfaces++;
                        }
                        else
                        {
                            foreach (var space in insideSpace)
                            {
                                innerSpace.Add(space);
                            }
                        }
                    }
                }

            }

            return freeSurfaces;
        }

        private (int, int, int) ParseInput(string input)
        {
            var result = input.Split(",");
            return (int.Parse(result[0]), int.Parse(result[1]), int.Parse(result[2]));
        }
    }
}
