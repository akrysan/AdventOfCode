using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Numerics;
using System.Runtime;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day13
    {
        public long Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\input\day13.txt");

            return RoundB(input);
        }

        private long RoundA(string input)
        {
            var input2 = input.Split("\r\n\r\n");
            var score = 0;

            for (int i = 0; i < input2.Length; i++)
            {
                var input4 = input2[i].Split("\r\n");
                if (Compare(input4[0], input4[1]) == -1)
                {
                    score += i + 1;
                }
            }

            return score;
        }

        private int RoundB(string input)
        {
            var input2 = input.Split("\r\n\r\n").SelectMany(x => x.Split("\r\n")).ToList();
            input2.Add("[[2]]");
            input2.Add("[[6]]");

            var input3 = input2.ToArray();

            Array.Sort(input3, new SignalComparer());

            var i = 0; var j = 0;
            for (int k = 0; k < input3.Length; k++)
            {
                if (input3[k] == "[[2]]")
                {
                    i = k + 1;
                }
                if (input3[k] == "[[6]]")
                {
                    j = k + 1;
                }
            }
 
            return i * j;
        }

        private int Compare(string left, string right)
        {
            if (left == "" && right == "")
            {
                return 0;
            }
            else if (left == "")
            {
                return -1;
            }

            if (right == "")
            {
                return 1;
            }

            if (int.TryParse(left, out var leftInt) && int.TryParse(right, out var rightInt))
            {
                if (leftInt < rightInt)
                {
                    return -1;
                }
                else if (leftInt == rightInt)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            string[] leftParsed;
            string[] rightParsed;
            if (left.StartsWith("["))
            {
                leftParsed = Parse(left);
            }
            else {
                leftParsed = new string[] { left };
            }

            if (right.StartsWith("["))
            {
                rightParsed = Parse(right);
            }
            else {
                rightParsed = new string[] { right };
            }

            for (int i = 0; i < leftParsed.Length || i < rightParsed.Length; i++)
            {
                var leftItem = i < leftParsed.Length ? leftParsed[i] : "";
                var rightItem = i < rightParsed.Length ? rightParsed[i] : "";
                var result = Compare(leftItem, rightItem);
                if (result != 0)
                {
                    return result;
                }
            }
            return 0;
        }

        private string[] Parse(string input)
        {
            var input2 = input;
            if (input.StartsWith("[")) {
                input2 = input.Substring(1, input.Length - 2);
            }
            var result = new List<string>();
            var tail = input2;
            var openArrayDeep = 0;
            var lastCommaIndex = 0;
            for (int i = 0; i < input2.Length; i++)
            {
                if (input2[i] == '[')
                {
                    openArrayDeep += 1;
                }
                else if (input[i] == ']')
                {
                    openArrayDeep -= 1;
                }

                if (input2[i] == ',' && openArrayDeep == 0)
                {
                    result.Add(input2.Substring(lastCommaIndex, i - lastCommaIndex).Trim(','));
                    lastCommaIndex = i;
                    tail = input2.Substring(i + 1, input2.Length - i - 1);
                }
            }

            result.Add(tail);

            return result.ToArray();
        }
    }

    public class SignalComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var left = x.ToString();
            var right = y.ToString();

            if (left == "" && right == "")
            {
                return 0;
            }
            else if (left == "")
            {
                return -1;
            }

            if (right == "")
            {
                return 1;
            }

            if (int.TryParse(left, out var leftInt) && int.TryParse(right, out var rightInt))
            {
                if (leftInt < rightInt)
                {
                    return -1;
                }
                else if (leftInt == rightInt)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            string[] leftParsed;
            string[] rightParsed;
            if (left.StartsWith("["))
            {
                leftParsed = Parse(left);
            }
            else
            {
                leftParsed = new string[] { left };
            }

            if (right.StartsWith("["))
            {
                rightParsed = Parse(right);
            }
            else
            {
                rightParsed = new string[] { right };
            }

            for (int i = 0; i < leftParsed.Length || i < rightParsed.Length; i++)
            {
                var leftItem = i < leftParsed.Length ? leftParsed[i] : "";
                var rightItem = i < rightParsed.Length ? rightParsed[i] : "";
                var result = Compare(leftItem, rightItem);
                if (result != 0)
                {
                    return result;
                }
            }
            return 0;
        }

        private string[] Parse(string input)
        {
            var input2 = input;
            if (input.StartsWith("["))
            {
                input2 = input.Substring(1, input.Length - 2);
            }
            var result = new List<string>();
            var tail = input2;
            var openArrayDeep = 0;
            var lastCommaIndex = 0;
            for (int i = 0; i < input2.Length; i++)
            {
                if (input2[i] == '[')
                {
                    openArrayDeep += 1;
                }
                else if (input[i] == ']')
                {
                    openArrayDeep -= 1;
                }

                if (input2[i] == ',' && openArrayDeep == 0)
                {
                    result.Add(input2.Substring(lastCommaIndex, i - lastCommaIndex).Trim(','));
                    lastCommaIndex = i;
                    tail = input2.Substring(i + 1, input2.Length - i - 1);
                }
            }

            result.Add(tail);

            return result.ToArray();
        }
    }
}
