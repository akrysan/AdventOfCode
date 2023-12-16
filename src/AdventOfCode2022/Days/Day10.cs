namespace AdventOfCode2022.Days
{
    class Day10
    {
        public int Solve()
        {
            var input = File.ReadAllText(@"..\..\..\Input\Day10.txt");

            return RoundA(input);
        }

        private int RoundA(string input)
        {
            var cycle = 0;
            var registerX = 1;
            var signalStrength = new List<int>(); ;
            foreach (var cmd in input.Split("\r\n"))
            {
                int param = 0;
                var maxCycle = 0;
                if (cmd == "noop")
                {
                    maxCycle = 1;
                }
                else
                {
                    param = int.Parse(cmd.Split(" ")[1]);
                    maxCycle = 2;
                }

                for (int i = 1; i <= maxCycle; i++)
                {
                    cycle++;

                    if (cycle == 20)
                    {
                        signalStrength.Add(20 * registerX);
                    }
                    else if (((cycle - 20) % 40) == 0)
                    {
                        signalStrength.Add((cycle) * registerX);
                    }
                }

                registerX += param;
            }

            return signalStrength.Take(6).Sum();
        }

        private int RoundB(string input)
        {
            var cycle = 0;
            var registerX = 1;
            var crt = new bool[240];
            foreach (var cmd in input.Split("\r\n"))
            {
                int param = 0;
                var maxCycle = 0;
                if (cmd == "noop")
                {
                    maxCycle = 1;
                }
                else
                {
                    param = int.Parse(cmd.Split(" ")[1]);
                    maxCycle = 2;
                }

                for (int i = 1; i <= maxCycle; i++)
                {
                    cycle++;

                    crt[cycle - 1] = registerX - 1 <= (cycle - 1) % 40 && (cycle - 1) % 40 <= registerX + 1;
                }

                registerX += param;
            }

            RenderImage(crt);

            return 0;
        }

        private void RenderImage(bool[] crt)
        {
            for (int i = 0; i < crt.Length; i++)
            {
                if ((i % 40) == 0)
                {
                    Console.WriteLine("");
                }
                Console.Write(crt[i] ? "X" : ".");
            }
        }
    }
}
