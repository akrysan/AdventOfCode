namespace AdventOfCode2022.Days
{
    class Day22
    {
        public long Solve()
        {
            var input = File.ReadAllText(@"..\..\..\Input\Day22.txt");

            return RoundA(input);
        }

        private long RoundA(string input)
        {
            var input2 = input.Split("\r\n\r\n").ToArray();
            var map = ParseMap(input2[0].Split("\r\n"));
            var path = input2[1];

            var position = (GetStartPositionX(map), 0, 0);

            var steps = "";
            foreach (var cmd in path)
            {
                if (Char.IsDigit(cmd))
                {
                    steps = steps + cmd;
                }
                else if (Char.IsLetter(cmd))
                {
                    position = Move(map, position, int.Parse(steps));
                    steps = "";
                    position = Rotate(position, cmd);
                }
            }
            position = Move(map, position, int.Parse(steps));


            return 4 * (position.Item1 + 1) + 1000 * (position.Item2 + 1) + position.Item3;
        }

        private long RoundB(string input)
        {

            return 0;
        }

        private char[,] ParseMap(string[] input) {
            var maxX = input.Max(x => x.Length);
            var maxY = input.Length;

            var map = new char[maxX, maxY];

            for(var i = 0; i < input.Length; i++)
            {
                for(var j = 0; j < input[i].Length; j++)
                {
                    map[j, i] = input[i][j] == ' ' ? (char)0 : input[i][j];
                }
            }

            return map;
        }

        private int GetStartPositionX(char[,] top)
        {
            for(var i = 0; i < top.GetLength(0); i++)
            {
                if (top[i, 0] == 0 && top[i + 1, 0] != 0)
                {
                    return i + 1;
                }
            }

            return 0; 
        }

        private (int, int, int) Move(char[,] map, (int, int, int) position, int steps)
        {

            var result = position;

            for (var i = 0; i < steps; i++)
            {
                var next = GetNextPosition(map, result);

                if (map[next.Item1, next.Item2] == '#')
                {
                    return result;
                }

                result = next;
            }

            return result;
        }

        private (int, int, int) GetNextPosition(char[,] map, (int, int, int) position)
        {
            if (position.Item3 == 0) // right
            {
                var next = 1;
                while (map[(position.Item1 + next) % map.GetLength(0), position.Item2] == (char)0)
                {
                    next++;
                }

                return ((position.Item1 + next) % map.GetLength(0), position.Item2, position.Item3);
            }
            else if (position.Item3 == 1) // down
            {
                var next = 1;
                while (map[position.Item1, (position.Item2 + next) % map.GetLength(1)] == (char)0)
                {
                    next++;
                }

                return (position.Item1, (position.Item2 + next) % map.GetLength(1), position.Item3);
            }
            else if (position.Item3 == 2) // left
            {
                var next = 1;
                while (map[(position.Item1 - next + map.GetLength(0)) % map.GetLength(0), position.Item2] == (char)0)
                {
                    next++;
                }

                return ((position.Item1 - next + map.GetLength(0)) % map.GetLength(0), position.Item2, position.Item3);
            }
            else // up
            {
                var next = 1;
                while (map[position.Item1, (position.Item2 - next + map.GetLength(1)) % map.GetLength(1)] == (char)0)
                {
                    next++;
                }

                return (position.Item1, (position.Item2 - next + map.GetLength(1)) % map.GetLength(1), position.Item3);
            }
        }

        private (int, int, int) Rotate((int, int, int) position, char cmd)
        {
            if (cmd == 'R')
            {
                position.Item3 = (position.Item3 + 1) % 4;
            }
            else {
                position.Item3 = (position.Item3 - 1 + 4) % 4;
            }

            return position;
        }
    }
}
