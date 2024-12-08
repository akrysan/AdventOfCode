namespace AdventOfCode2024.Days
{
    class Day1
    {
        public long Solve()
        {
            var input = File.ReadAllText(@"..\..\..\Input\Day1.txt");

            return RoundB(input);
        }

        private long RoundA(string input)
        {
            var result = 0;
            var leftList = new List<int>();
            var rightList = new List<int>();
            foreach (var line in input.Split(Environment.NewLine))
            {
                var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                leftList.Add(split[0]);
                rightList.Add(split[1]);
            }

            leftList.Sort();
            rightList.Sort();

            result = leftList.Select((x, i) => Math.Abs(rightList[i] - x)).Sum();

            return result;
        }

        private long RoundB(string input)
        {
            var result = 0;
            var leftList = new List<int>();
            var rightList = new List<int>();
            foreach (var line in input.Split(Environment.NewLine))
            {
                var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                leftList.Add(split[0]);
                rightList.Add(split[1]);
            }

            leftList.Sort();
            rightList.Sort();

            result = leftList.Select(x => x * rightList.Count(y => x == y)).Sum();

            return result;
        }

    }
}
