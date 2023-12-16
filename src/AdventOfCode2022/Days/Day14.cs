using System.Data;

namespace AdventOfCode2022.Days
{
    class Day14
    {
        public long Solve()
        {
            var input = File.ReadAllText(@"..\..\..\Input\Day14.txt");

            return RoundB(input);
        }

        private long RoundA(string input)
        {
            var cave = new char[1000, 1000];
            var input2 = input.Split("\r\n");

            foreach (var line in input2)
            {
                ParseInput(cave, line);
            }

            int cycle = 0;

            while(DropSand(cave, (500, 0)))
            {
                cycle++;
            }

            return cycle;
        }

        private int RoundB(string input)
        {
            var cave = new char[1000, 1000];
            var input2 = input.Split("\r\n");

            foreach (var line in input2)
            {
                ParseInput(cave, line);
            }

            var floor = 0;
            for (int i = 0; i < cave.GetLength(0); i++)
            {
                for (int j = 0; j < cave.GetLength(1); j++)
                {
                    if (cave[i, j] == '#' && floor < j)
                    {
                        floor = j;
                    }
                }
            }

            floor += 2;

            for (int i = 0; i < cave.GetLength(0); i++)
            {
                cave[i, floor] = '#';
            }

            int cycle = 0;

            while (DropSand(cave, (500, 0)))
            {
                cycle++;
            }

            return cycle;
        }

        private void ParseInput(char[,] cave, string scan)
        {
            var result = scan.Split(" -> ");
            for (int i = 1; i < result.Length; i++)
            {
                var point = result[i - 1].Split(",");
                var start = (int.Parse(point[0]), int.Parse(point[1]));
                point = result[i].Split(",");
                var end = (int.Parse(point[0]), int.Parse(point[1]));

                if (start.Item1 > end.Item1 || start.Item2 > end.Item2)
                {
                    var interim = end;
                    end = start;
                    start = interim;
                }
                for (int k = start.Item1; k <= end.Item1; k++)
                {
                    for (int l = start.Item2; l <= end.Item2; l++)
                    {
                        cave[k, l] = '#';
                    }
                }
            }
        }

        private bool DropSand(char[,] cave, (int, int) start)
        {
            if (cave[start.Item1, start.Item2] == 'O')
            {
                return false;
            }

            for (int i = start.Item2; i < cave.GetLength(1) - 1; i++)
            {
                if (cave[start.Item1, i + 1] == '#' || cave[start.Item1, i + 1] == 'O')
                {
                    var possibleTarget = new (int, int)[] { (start.Item1 - 1, i + 1), (start.Item1 + 1, i + 1) }
                        .Where(x => !new char[] { '#', 'O' }.Contains(cave[x.Item1, x.Item2]));
                    var nextTarget = possibleTarget.FirstOrDefault();
                    if (nextTarget == (0, 0))
                    {
                        cave[start.Item1, i] = 'O';
                        return true;
                    }
                    return DropSand(cave, nextTarget);
                }
            }

            return false;
        }
    }
}
