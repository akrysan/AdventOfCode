namespace AdventOfCode2022.Days
{
    class Day4
    {
        public int Solve()
        {
            var input = File.ReadAllText(@"..\..\..\Input\Day4.txt");

            return RoundB(input);
        }

        private int RoundA(string input)
        {
            var result = input
                .Split("\r\n")
                .Select(Parse)
                .Where(x => (x[2] <= x[0] && x[1] <= x[3]) || (x[0] <= x[2] && x[3] <= x[1]))
                .Count();

            return result;
        }

        private int RoundB(string input)
        {
            var result = input
                .Split("\r\n")
                .Select(Parse)
                .Where(x => !(x[3] < x[0]) && !(x[1] < x[2]))
                .Count();

            return result;
        }

        private int[] Parse(string x) {
            var result = x.Split(",")
            .SelectMany(y => y.Split("-"))
            .Select(y => int.Parse(y));

            return result.ToArray();
        }
    }
}
