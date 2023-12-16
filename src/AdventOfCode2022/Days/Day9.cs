using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;

namespace AdventOfCode2022.Days
{
    class Day9
    {
        public int Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\Input\Day9.txt");

            return RoundB(input);
        }

        private int RoundA(string input)
        {
            var map = new List<(int, int)>(); //HashSet<(int, int)>();
            var headPosition = (0, 0);
            var tailPosition = (0, 0);
            var moves = input.Split("\r\n");

            foreach (var move in moves)
            {
                for (int i = 1; i <= int.Parse(move.Split(" ")[1].ToString()); i++)
                {
                    if (move[0] == 'U')
                    {
                        headPosition = (headPosition.Item1, headPosition.Item2 + 1);
                    }
                    else if (move[0] == 'D')
                    {
                        headPosition = (headPosition.Item1, headPosition.Item2 - 1);
                    }
                    else if (move[0] == 'R')
                    {
                        headPosition = (headPosition.Item1 + 1, headPosition.Item2);
                    }
                    else if (move[0] == 'L')
                    {
                        headPosition = (headPosition.Item1 - 1, headPosition.Item2);
                    }

                    map.AddRange(MoveTail(moves, headPosition, tailPosition));
                }
            }

            return map.Distinct().Count();
        }

        private int RoundB(string input)
        {
            var moves = input.Split("\r\n");



            return 0;
        }

        private List<(int, int)> MoveTail(string[] moves, (int, int) head, (int, int) tail)
        {
            var map = new List<(int, int)>(); //HashSet<(int, int)>();
            var rope = new (int, int)[10];

            foreach (var move in moves)
            {
                for (int i = 1; i <= int.Parse(move.Split(" ")[1].ToString()); i++)
                {
                    if (move[0] == 'U')
                    {
                        rope[0] = (rope[0].Item1, rope[0].Item2 + 1);
                    }
                    else if (move[0] == 'D')
                    {
                        rope[0] = (rope[0].Item1, rope[0].Item2 - 1);
                    }
                    else if (move[0] == 'R')
                    {
                        rope[0] = (rope[0].Item1 + 1, rope[0].Item2);
                    }
                    else if (move[0] == 'L')
                    {
                        rope[0] = (rope[0].Item1 - 1, rope[0].Item2);
                    }

                    for (int j = 1; j < rope.Length; j++)
                    {
                        //rope[j] = Movel(rope[j - 1], rope[j]);
                        if (int.Abs(head.Item1 - tail.Item1) > 1 && int.Abs(head.Item2 - tail.Item2) > 1)
                        {
                            rope[j] = (tail.Item1 + (int.Abs(head.Item1 - tail.Item1) / (head.Item1 - tail.Item1)), tail.Item2 + (int.Abs(head.Item2 - tail.Item2) / (head.Item2 - tail.Item2)));
                        }
                        else if (int.Abs(head.Item2 - tail.Item2) > 1)
                        {
                            rope[j] = (head.Item1, tail.Item2 + (int.Abs(head.Item2 - tail.Item2) / (head.Item2 - tail.Item2)));
                        }
                        else if (int.Abs(head.Item1 - tail.Item1) > 1)
                        {
                            rope[j] = (tail.Item1 + (int.Abs(head.Item1 - tail.Item1) / (head.Item1 - tail.Item1)), head.Item2);
                        }
                        else
                        {
                            rope[j] = (tail.Item1, tail.Item2);
                        }
                    }

                    map.Add(rope[rope.Length - 1]);
                }
            }

            return map;
        }
    }
}
