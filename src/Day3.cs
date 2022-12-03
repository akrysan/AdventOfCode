using System;
using System.Linq;

namespace AdventOfCode
{
    class Day3
    {
        public int Solve()
        {
            var input = System.IO.File.ReadAllText(@"input\day3.txt");

            return RoundB(input);
        }

        private int RoundA(string input)
        {
            var result = input
                .Split("\r\n")
                .Select(y =>
                {
                    var compartments = (y.Substring(0, y.Length / 2), y.Substring(y.Length / 2));

                    char type = 'a';
                    foreach (var z in compartments.Item1)
                    {
                        if (compartments.Item2.Contains(z))
                        {
                            type = z;
                            break;
                        }
                    }

                    return type;
                })
                .Select(score)
                .Sum();

            return result;
        }

        private int RoundB(string input)
        {
            var result = input
                .Split("\r\n");

            var sum = 0;

            for (int i = 0; i < result.Length / 3; i++)
            {
                char type = 'a';
                foreach (var z in result[3 * i])
                {
                    if (result[3 * i + 1].Contains(z) && result[3 * i + 2].Contains(z))
                    {
                        type = z;
                        break;
                    }
                }

                sum += score(type);
            }

            return sum;
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
