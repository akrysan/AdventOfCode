using System;
using System.Linq;

namespace AdventOfCode
{
    class Day3
    {
        public int Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\input\day3.txt");

            return RoundB(input);
        }

        private int RoundA(string input)
        {
            var result = input
                .Split("\r\n")
                .Select(x =>
                {
                    var y = (x.Substring(0, x.Length / 2), x.Substring(x.Length / 2));

                    foreach (var z in y.Item1)
                    {
                        if (y.Item2.Contains(z))
                        {
                            return z;
                        }
                    }

                    return Char.MinValue;
                })
                .Select(score)
                .Sum();

            return result;
        }

        private int RoundB(string input)
        {
            var result = input
                .Split("\r\n")
                .Chunk(3)
                .Select(x => {
                    foreach (var y in x[0])
                    {
                        if (x[1].Contains(y) && x[2].Contains(y))
                        {
                            return y;
                        }
                    }

                    return Char.MinValue;
                })
                .Select(score)
                .Sum();

            return result;
        }

        private int score(char c)
        {
            if ('a' <= c && c <= 'z')
            {
                return (int)c - (int)'a' + 1;
            }
            else if ('A' <= c && c <= 'Z')
            {
                return (int)c - (int)'A' + 27;
            }
            else
            {
                return 0;
            }
        }
    }
}
