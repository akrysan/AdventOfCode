using System.Linq;

namespace AdventOfCode2022.Days
{
    class Day1
    {
        public int Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\Input\Day1.txt");

            var maxCalories = input
                .Split("\r\n\r\n")
                .Select(x => x.Split("\r\n"))
                .Select(y => y.Select(z => int.Parse(z)).Sum())
                .OrderByDescending(x => x)
                .Take(3)
                .Sum();


            return maxCalories;
        }
    }
}
