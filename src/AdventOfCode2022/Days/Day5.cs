using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Days
{
    class Day5
    {
        public int Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\Input\Day5.txt");

            return RoundB(input);
        }

        private int RoundA(string input)
        {
            var result = input
                .Split("\r\n\r\n");

            Stack<char>[] stack = new Stack<char>[] { new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>() };

            foreach (var inp in result[0].Split("\r\n").Reverse().Skip(1))
            {
                if (inp[1] != ' ') {
                    stack[0].Push(inp[1]);
                }
                if (inp[5] != ' ')
                {
                    stack[1].Push(inp[5]);
                }
                if (inp[9] != ' ')
                {
                    stack[2].Push(inp[9]);
                }
                if (inp[13] != ' ')
                {
                    stack[3].Push(inp[13]);
                }
                if (inp[17] != ' ')
                {
                    stack[4].Push(inp[17]);
                }
                if (inp[21] != ' ')
                {
                    stack[5].Push(inp[21]);
                }
                if (inp[25] != ' ')
                {
                    stack[6].Push(inp[25]);
                }
                if (inp[29] != ' ')
                {
                    stack[7].Push(inp[29]);
                }
                if (inp[33] != ' ')
                {
                    stack[8].Push(inp[33]);
                }
            }


            foreach (var cmd in result[1].Split("\r\n"))
            {
                var cmd2 = cmd.Split(" ");
                var from = int.Parse(cmd2[3]) - 1;
                var to = int.Parse(cmd2[5]) - 1;

                for (int i = 1; i <= int.Parse(cmd2[1]); i++)
                {
                    var item = stack[from].Pop();
                    stack[to].Push(item);
                }
                
            }

            foreach (var st in stack) {
                Console.Write(st.Pop());
            }

            return 0;
        }

        private int RoundB(string input)
        {
            var result = input
                .Split("\r\n\r\n");

            Stack<char>[] stack = new Stack<char>[] { new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>() };

            foreach (var inp in result[0].Split("\r\n").Reverse().Skip(1))
            {
                if (inp[1] != ' ')
                {
                    stack[0].Push(inp[1]);
                }
                if (inp[5] != ' ')
                {
                    stack[1].Push(inp[5]);
                }
                if (inp[9] != ' ')
                {
                    stack[2].Push(inp[9]);
                }
                if (inp[13] != ' ')
                {
                    stack[3].Push(inp[13]);
                }
                if (inp[17] != ' ')
                {
                    stack[4].Push(inp[17]);
                }
                if (inp[21] != ' ')
                {
                    stack[5].Push(inp[21]);
                }
                if (inp[25] != ' ')
                {
                    stack[6].Push(inp[25]);
                }
                if (inp[29] != ' ')
                {
                    stack[7].Push(inp[29]);
                }
                if (inp[33] != ' ')
                {
                    stack[8].Push(inp[33]);
                }
            }


            foreach (var cmd in result[1].Split("\r\n"))
            {
                var cmd2 = cmd.Split(" ");
                var from = int.Parse(cmd2[3]) - 1;
                var to = int.Parse(cmd2[5]) - 1;


                var a = new List<char>();
                for (int i = 1; i <= int.Parse(cmd2[1]); i++)
                {
                    a.Add(stack[from].Pop());
                }

                a.Reverse();
                foreach (var b in a) {
                    stack[to].Push(b);
                }

            }

            foreach (var st in stack)
            {
                Console.Write(st.Pop());
            }

            return 0;
        }

        private int Parse(string x)
        {
            return 0;
        }
    }
}
