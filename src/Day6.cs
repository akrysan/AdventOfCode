using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day6
    {
        public int Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\input\day6.txt");

            return RoundA(input);
        }

        private int RoundA(string input)
        {
            var a = 0;
            for (var i = 0; i <= input.Length; i++) {
                if (input.Substring(i, 14).ToArray().Distinct().Count() == 14) {
                    a = i + 14;
                    break;
                }
            }

            return a;
        }

        private int RoundB(string input)
        {
            var result = input
                .Split("\r\n")
                .Select(Parse)
                .Count();

            return result;
        }

        private int Parse(string x)
        {
            return 0;
        }
    }
}
