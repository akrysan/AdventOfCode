using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2022.Days
{
    class Day8
    {
        public int Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\Input\Day8.txt");

            return RoundB(input);
        }

        private int RoundA(string input)
        {
            var map = Parse(input);
            var visibleTree = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {

                    var isVisible = false;

                    if (i == 0 || j == 0 || i == map.GetLength(0) - 1 || j == map.GetLength(1) - 1)
                    {
                        visibleTree++;
                        isVisible = true;
                    }

                    if (isVisible)
                    {
                        continue;
                    }

                    for (int k = i - 1; k >= 0; k--)
                    {
                        if (map[i, j] <= map[k, j])
                        {
                            break;
                        }
                        if (k == 0)
                        {
                            visibleTree++;
                            isVisible = true;
                        }
                    }

                    if (isVisible)
                    {
                        continue;
                    }

                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (map[i, j] <= map[i, k])
                        {
                            break;
                        }
                        if (k == 0)
                        {
                            visibleTree++;
                            isVisible = true;
                        }
                    }

                    if (isVisible)
                    {
                        continue;
                    }

                    for (int k = i + 1; k < map.GetLength(0); k++)
                    {
                        if (map[i, j] <= map[k, j])
                        {
                            break;
                        }
                        if (k == map.GetLength(0) - 1)
                        {
                            visibleTree++;
                            isVisible = true;
                        }
                    }

                    if (isVisible)
                    {
                        continue;
                    }

                    for (int k = j + 1; k < map.GetLength(1); k++)
                    {
                        if (map[i, j] <= map[i, k])
                        {
                            break;
                        }
                        if (k == map.GetLength(1) - 1)
                        {
                            visibleTree++;
                            isVisible = true;
                        }
                    }

                    if (isVisible)
                    {
                        continue;
                    }
                }
            }

            return visibleTree;
        }

        private int RoundB(string input)
        {
            {
                var map = Parse(input);
                var score = 0;

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        var a = 0; var b = 0; var c = 0; var d = 0;

                        for (int k = i - 1; k >= 0; k--)
                        {
                            a++;
                            if (map[i, j] <= map[k, j])
                            {
                                break;
                            }
                        }

                        for (int k = j - 1; k >= 0; k--)
                        {
                            b++;
                            if (map[i, j] <= map[i, k])
                            {
                                break;
                            }
                        }

                        for (int k = i + 1; k < map.GetLength(0); k++)
                        {
                            c++;
                            if (map[i, j] <= map[k, j])
                            {
                                break;
                            }
                        }

                        for (int k = j + 1; k < map.GetLength(1); k++)
                        {
                            d++;
                            if (map[i, j] <= map[i, k])
                            {
                                break;
                            }
                        }

                        if (score < a * b * c * d) {
                            score = a * b * c * d;
                        }
                    }
                }

                return score;
            }
        }

        private int[,] Parse(string input)
        {
            var rows = input.Split("\r\n");
            int[,] map = new int[rows.Length, rows[0].Length];
            for (int i = 0; i < rows[0].Length; i++)
            {
                for (int j = 0; j < rows.Length; j++)
                {
                    map[i, j] = rows[i][j] - '0';
                }
            }

            return map;
        }
    }
}
