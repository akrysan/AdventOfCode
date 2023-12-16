using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Days
{
    class Day23
    {
        public long Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\Input\Day23.txt");

            return RoundB(input);
        }

        private long RoundA(string input)
        {
            var elves = ParseElves(input.Split("\r\n").ToArray());

            var round = (long)0;
            while (round < 10)
            {
                Move(elves, round);
                round++;
            }

            return CalculateResult(elves);
        }

        private long RoundB(string input)
        {
            var elves = ParseElves(input.Split("\r\n").ToArray());

            var round = (long)0;
            while (Move(elves, round))
            {
                round++;
            }

            return round + 1;
        }

        private HashSet<(int, int)> ParseElves(string[] input)
        {
            var elves = new HashSet<(int, int)>();
            var maxX = input.Max(x => x.Length);
            var maxY = input.Length;

            var map = new char[maxX, maxY];

            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '#')
                    {
                        elves.Add((j, i));
                    }
                }
            }

            return elves;
        }

        private bool Move(HashSet<(int, int)> elves, long round)
        {
            var possiblyMoves = new Dictionary<(int, int), (int, int)>();
            var blockedPositions = new HashSet<(int, int)>();

            // round 1
            foreach (var elf in elves)
            {
                var otherElves = false;
                for (var i = 0; i < 4; i++)
                {
                    otherElves = otherElves || GetPossibleMoves(elf, round + i).Any(x => elves.Contains(x));
                    if (otherElves)
                    {
                        break;
                    }
                }

                if (!otherElves)
                {
                    continue; ;
                }

                for (var i = 0; i < 4; i++)
                {
                    var elfMoves = GetPossibleMoves(elf, round + i);
                    if (elfMoves.All(x => !elves.Contains(x)))
                    {
                        if (blockedPositions.Contains(elfMoves[1]))
                        {
                            break;
                        }

                        if (possiblyMoves.ContainsKey(elfMoves[1]))
                        {
                            blockedPositions.Add(elfMoves[1]);
                            possiblyMoves.Remove(elfMoves[1]);
                            break;
                        }

                        possiblyMoves.Add(elfMoves[1], elf);
                        break;
                    }
                }
            }

            // round 2
            foreach (var move in possiblyMoves)
            {
                elves.Remove(move.Value);
                elves.Add(move.Key);
            }

            return possiblyMoves.Count != 0;
        }

        private (int, int)[] GetPossibleMoves((int, int) elf, long round)
        {
            var moves = new (int, int)[0];

            if ((round % 4) == 0)
            {
                moves = new (int, int)[] {
                    (elf.Item1 - 1, elf.Item2 - 1),
                    (elf.Item1, elf.Item2 - 1),
                    (elf.Item1 + 1 , elf.Item2 - 1),
                };
            }
            else if ((round % 4) == 1)
            {
                moves = new (int, int)[] {
                    (elf.Item1 - 1, elf.Item2 + 1),
                    (elf.Item1, elf.Item2 + 1),
                    (elf.Item1 + 1 , elf.Item2 + 1),
                };
            }
            else if ((round % 4) == 2)
            {
                moves = new (int, int)[] {
                    (elf.Item1 - 1, elf.Item2 - 1),
                    (elf.Item1 - 1, elf.Item2),
                    (elf.Item1 - 1 , elf.Item2 + 1),
                };
            }
            else if ((round % 4) == 3)
            {
                moves = new (int, int)[] {
                    (elf.Item1 + 1, elf.Item2 - 1),
                    (elf.Item1 + 1, elf.Item2),
                    (elf.Item1 + 1 , elf.Item2 + 1),
                };
            }

            return moves;
        }

        private int CalculateResult(HashSet<(int, int)> elves)
        {
            var topLeft = elves.First();
            var downRight = elves.First();

            foreach (var elf in elves)
            {
                if (elf.Item1 < topLeft.Item1)
                {
                    topLeft = (elf.Item1, topLeft.Item2);
                }
                if (elf.Item2 < topLeft.Item2)
                {
                    topLeft = (topLeft.Item1, elf.Item2);
                }
                if (downRight.Item1 < elf.Item1)
                {
                    downRight = (elf.Item1, downRight.Item2);
                }
                if (downRight.Item2 < elf.Item2)
                {
                    downRight = (downRight.Item1, elf.Item2);
                }
            }

            return ((downRight.Item1 - topLeft.Item1 + 1) * (downRight.Item2 - topLeft.Item2 + 1)) - elves.Count;
        }

        private void PrintElves(HashSet<(int, int)> elves)
        {
            var map = new char[20, 20];
            foreach (var elf in elves)
            {
                map[elf.Item1, elf.Item2] = '#';
            }

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[j, i] == '#' ? "#" : ".");
                }
                Console.WriteLine("");
            }
        }
    }
}
