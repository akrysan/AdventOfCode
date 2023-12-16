using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Days
{
    class Day25
    {
        public string Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\Input\Day25.txt");

            return RoundA(input);
        }

        private string RoundA(string input)
        {
            var result = input.Split("\r\n").Select(ParseInput).Sum();
            return result.ToString();
        }

        private Snafu ParseInput(string input) => new Snafu(input);
    }

    public class Snafu
    {
        string _snafu = "0";

        public Snafu() { }

        public Snafu(string snafu)
        {
            _snafu = snafu;
        }
        public static Snafu operator +(Snafu a, Snafu b)
        {
            var addMatrix = new Dictionary<(char, char), (char, char)> {
                { ('=', '='), ('-', '1')},
                { ('=', '-'), ('-', '2')},
                { ('=', '0'), ('0', '=')},
                { ('=', '1'), ('0', '-')},
                { ('=', '2'), ('0', '0')},
                { ('-', '='), ('-', '2')},
                { ('-', '-'), ('0', '=')},
                { ('-', '0'), ('0', '-')},
                { ('-', '1'), ('0', '0')},
                { ('-', '2'), ('0', '1')},
                { ('0', '='), ('0', '=')},
                { ('0', '-'), ('0', '-')},
                { ('0', '0'), ('0', '0')},
                { ('0', '1'), ('0', '1')},
                { ('0', '2'), ('0', '2')},
                { ('1', '='), ('0', '-')},
                { ('1', '-'), ('0', '0')},
                { ('1', '0'), ('0', '1')},
                { ('1', '1'), ('0', '2')},
                { ('1', '2'), ('1', '=')},
                { ('2', '='), ('0', '0')},
                { ('2', '-'), ('0', '1')},
                { ('2', '0'), ('0', '2')},
                { ('2', '1'), ('1', '=')},
                { ('2', '2'), ('1', '-')},
            };

            var c = a.ToString().Reverse().ToArray();
            var d = b.ToString().Reverse().ToArray();
            var resultTotal = new List<char>();
            var maxLength = Math.Max(c.Length, d.Length);
            var prevIncrement = '0';

            for (var i = 0; i < maxLength; i++)
            {
                var e = i < c.Length ? c[i] : '0';
                var f = i < d.Length ? d[i] : '0';
                (var increment, var resultRegister) = addMatrix[(e, f)];
                (var increment2, resultRegister) = addMatrix[(resultRegister, prevIncrement)];
                resultTotal.Add(resultRegister);
                (_, prevIncrement) = addMatrix[(increment, increment2)];
            }

            if (prevIncrement != '0')
            {
                resultTotal.Add(prevIncrement);
            }

            resultTotal.Reverse();
            return new Snafu(string.Join("", resultTotal));
        }

        public override string ToString()
        {
            return _snafu;
        }
    }

    public static class Extensions
    {
        public static Snafu Sum(this IEnumerable<Snafu> list)
        {
            var result = new Snafu();
            foreach (var item in list)
            {
                result += item;
            }

            return result;
        }
    }
}
