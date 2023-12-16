using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day24
    {
        public static int MaxX;
        public static int MaxY;

        public long Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\input\day24.txt");

            return RoundB(input);
        }

        private long RoundA(string input)
        {
            var blizzards = ParseInput(input);

            var variants = new HashSet<(int, int)>();
            variants.Add((0, -1));
            var time1 = PassGrove(blizzards, variants, (MaxX - 1, MaxY - 1));


            return time1;
        }

        private long RoundB(string input)
        {
            var blizzards = ParseInput(input);

            var variants = new HashSet<(int, int)>();
            variants.Add((0, -1));
            var time1 = PassGrove(blizzards, variants, (MaxX - 1, MaxY - 1));

            variants = new HashSet<(int, int)>();
            variants.Add((MaxX - 1, MaxY));
            var time2 = PassGrove(blizzards, variants, (0, 0));

            variants = new HashSet<(int, int)>();
            variants.Add((0, -1));
            var time3 = PassGrove(blizzards, variants, (MaxX - 1, MaxY - 1));

            return time1 + time2 + time3;
        }

        private int PassGrove(List<(int, int, int)> blizzards, HashSet<(int, int)> variants, (int, int) endPoint)
        {
            int round = 1;
            while (true)
            {
                MoveBlizzards(blizzards);
                var removeVariants = new HashSet<(int, int)>();
                var addVariants = new HashSet<(int, int)>();

                foreach (var variant in variants)
                {
                    if (variant == endPoint)
                    {
                        return round;
                    }

                    if (blizzards.Any(x => x.Item1 == variant.Item1 && x.Item2 == variant.Item2))
                    {
                        removeVariants.Add(variant);
                    }

                    var nextMoves = new (int, int)[] {
                        (variant.Item1 + 1, variant.Item2),
                        (variant.Item1, variant.Item2 + 1),
                        (variant.Item1 - 1, variant.Item2),
                        (variant.Item1, variant.Item2 - 1),
                    }.Where(x => 0 <= x.Item1 && x.Item1 < MaxX && 0 <= x.Item2 && x.Item2 < MaxY).ToArray();

                    foreach (var move in nextMoves)
                    {
                        if (!blizzards.Any(x => x.Item1 == move.Item1 && x.Item2 == move.Item2))
                        {
                            addVariants.Add(move);
                        }
                    }

                    if (variant == (0, -1) && addVariants.Count > 0)
                    {
                        removeVariants.Add(variant);
                    }
                }

                foreach (var variant in removeVariants)
                {
                    variants.Remove(variant); ;
                }

                foreach (var variant in addVariants)
                {
                    variants.Add(variant); ;
                }

                round++;
            }

        }

        private List<(int, int, int)> ParseInput(string input)
        {
            var input2 = input.Split("\r\n");
            var blizzards = new List<(int, int, int)>();

            for (var i = 1; i < input2.Length - 1; i++)
            {
                for (var j = 1; j < input2[i].Length - 1; j++)
                {
                    if (input2[i][j] != '.')
                    {
                        var direction = input2[i][j] switch
                        {
                            '>' => 0,
                            'v' => 1,
                            '<' => 2,
                            '^' => 3
                        };
                        blizzards.Add((j - 1, i - 1, direction));
                    }
                }
            }
            MaxX = input2[0].Length - 2;
            MaxY = input2.Length - 2;

            return blizzards;
        }

        private void MoveBlizzards(List<(int, int, int)> blizzards)
        {
            for (var i = 0; i < blizzards.Count; i++)
            {
                var blizzard = blizzards[i];
                blizzards[i] = blizzard.Item3 switch
                {
                    0 => ((blizzard.Item1 + 1) % MaxX, blizzard.Item2, blizzard.Item3),
                    1 => (blizzard.Item1, (blizzard.Item2 + 1) % MaxY, blizzard.Item3),
                    2 => ((blizzard.Item1 - 1 + MaxX) % MaxX, blizzard.Item2, blizzard.Item3),
                    3 => (blizzard.Item1, (blizzard.Item2 - 1 + MaxY) % MaxY, blizzard.Item3),
                };
            }
        }
    }
}
