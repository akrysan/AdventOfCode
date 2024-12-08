using System.Linq;

namespace AdventOfCode2024.Days;

class Day8
{

    public long Solve()
    {
        var input = File.ReadAllText(@"..\..\..\Input\Day8.txt");

        return RoundB(input);
    }

    private long RoundA(string input)
    {
        return Solve(input, AddAntinodesA);
    }

    private long RoundB(string input)
    {
        return Solve(input, AddAntinodesB);
    }

    private long Solve(string input, Action<(int, int), (int, int), List<(int, int)>, char[][]> addAntinodes)
    {
        long result = 0;

        var map = input.Split(Environment.NewLine)
                .Select(x => x.ToCharArray()).ToArray();

        var antennas = new Dictionary<char, List<(int, int)>>();
        var antinodes = new Dictionary<char, List<(int, int)>>();

        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[0].Length; j++)
            {
                var frequency = map[i][j];
                if (frequency != '.')
                {
                    if (!antennas.ContainsKey(frequency))
                    {
                        antennas.Add(frequency, []);
                        antinodes.Add(frequency, []);
                    }

                    foreach (var antenna in antennas[frequency])
                    {
                        addAntinodes((i, j), (i - antenna.Item1, j - antenna.Item2), antinodes[frequency], map);
                        addAntinodes(antenna, (antenna.Item1 - i, antenna.Item2 - j), antinodes[frequency], map);
                    }

                    antennas[frequency].Add((i, j));
                }
            }
        }

        result = antinodes.SelectMany(x => x.Value).Distinct().Count();

        return result;
    }

    private static void AddAntinodesA((int, int) antenna, (int, int) delta, List<(int, int)> antinodes, char[][] map)
    {
        var antinode = (antenna.Item1 + delta.Item1, antenna.Item2 + delta.Item2);
        if (0 <= antinode.Item1 && antinode.Item1 < map.Length &&
            0 <= antinode.Item2 && antinode.Item2 < map[0].Length &&
            !antinodes.Contains(antinode))
        {
            antinodes.Add(antinode);
        }
    }

    private static void AddAntinodesB((int, int) antenna, (int, int) delta, List<(int, int)> antinodes, char[][] map)
    {
        var antinode = antenna;

        while (0 <= antinode.Item1 && antinode.Item1 < map.Length &&
            0 <= antinode.Item2 && antinode.Item2 < map[0].Length)
        {
            if (!antinodes.Contains(antinode))
            {
                antinodes.Add(antinode);
            }

            antinode = (antinode.Item1 + delta.Item1, antinode.Item2 + delta.Item2);
        }
    }
}
