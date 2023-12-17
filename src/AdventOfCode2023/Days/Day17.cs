namespace AdventOfCode2023.Days
{
    class Day17
    {
        public long Solve()
        {
            var input = File.ReadAllText(@"..\..\..\Input\Day17.txt");

            return RoundB(input);
        }

        private long RoundA(string input)
        {
            return Round(input, 1, 3);
        }

        private long RoundB(string input)
        {
            return Round(input, 4, 10);
        }

        private long Round(string input, int minStraight, int maxStraight)
        {
            long result = 0;

            var city = input.Split(Environment.NewLine)
                .Select(x => x.Select(y => y - '0').ToArray()).ToArray();

            var heatLoss = new Dictionary<(int X, int Y, char Direction, int Count), long>();

            var queue = new Queue<(int X, int Y, long Heat, char Direction, int Count)>();

            queue.Enqueue((0, 1, 0, 'R', 1));
            queue.Enqueue((1, 0, 0, 'D', 1));

            do
            {
                var pos = queue.Dequeue();
                pos.Heat += city[pos.X][pos.Y];
                if (heatLoss.TryGetValue((pos.X, pos.Y, pos.Direction, pos.Count), out var heat))
                {
                    if (pos.Heat < heat)
                    {
                        heatLoss[(pos.X, pos.Y, pos.Direction, pos.Count)] = pos.Heat;
                        NextPositions(pos, city.Length, city[0].Length, minStraight, maxStraight).ForEach(x => queue.Enqueue(x));
                    }
                }
                else
                {
                    heatLoss.Add((pos.X, pos.Y, pos.Direction, pos.Count), pos.Heat);
                    NextPositions(pos, city.Length, city[0].Length, minStraight, maxStraight).ForEach(x => queue.Enqueue(x));
                }


            }
            while (queue.Count() != 0);

            result = new int[] { 1, 2, 3, 5, 6, 7, 8, 9, 10 }.SelectMany(x => new char[] { 'D', 'R' }.Select(y =>
            {
                var heat = (long)0;
                heatLoss.TryGetValue((city.Length - 1, city[0].Length - 1, y, x), out heat);
                return heat;

            })).Where(x => x != 0).Min();

            return result;
        }

        private static List<(int X, int Y, long Heat, char Direction, int Count)> NextPositions(
            (int X, int Y, long Heat, char Direction, int Count) position,
            int maxX,
            int maxY,
            int minStraight,
            int maxStraight)
        {
            (int X, int Y, char Direction) direction = (0, 0, char.MinValue);
            var result = new List<(int X, int Y, long Heat, char Direction, int Count)>();

            // left
            if (position.Direction == 'R')
            {
                direction = (position.X - 1, position.Y, 'U');
            }
            else if (position.Direction == 'D')
            {
                direction = (position.X, position.Y + 1, 'R');
            }
            else if (position.Direction == 'L')
            {
                direction = (position.X + 1, position.Y, 'D');
            }
            else if (position.Direction == 'U')
            {
                direction = (position.X, position.Y - 1, 'L');
            }

            if ((0 <= direction.X && direction.X < maxX && 0 <= direction.Y && direction.Y < maxY) &&
                (position.Count >= minStraight))
            {
                result.Add((direction.X, direction.Y, position.Heat, direction.Direction, 1));
            }

            // right
            if (position.Direction == 'R')
            {
                direction = (position.X + 1, position.Y, 'D');
            }
            else if (position.Direction == 'D')
            {
                direction = (position.X, position.Y - 1, 'L');
            }
            else if (position.Direction == 'L')
            {
                direction = (position.X - 1, position.Y, 'U');
            }
            else if (position.Direction == 'U')
            {
                direction = (position.X, position.Y + 1, 'R');
            }

            if ((0 <= direction.X && direction.X < maxX && 0 <= direction.Y && direction.Y < maxY) &&
                (position.Count >= minStraight))
            {
                result.Add((direction.X, direction.Y, position.Heat, direction.Direction, 1));
            }

            // stright
            if (position.Direction == 'R')
            {
                direction = (position.X, position.Y + 1, 'R');
            }
            else if (position.Direction == 'D')
            {
                direction = (position.X + 1, position.Y, 'D');
            }
            else if (position.Direction == 'L')
            {
                direction = (position.X, position.Y - 1, 'L');
            }
            else if (position.Direction == 'U')
            {
                direction = (position.X - 1, position.Y, 'U');
            }

            if (0 <= direction.X && direction.X < maxX && 0 <= direction.Y && direction.Y < maxY && position.Count < maxStraight)
            {
                result.Add((direction.X, direction.Y, position.Heat, direction.Direction, position.Count + 1));
            }

            return result;
        }
    }
}
