using System.Data;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Days
{
    class Day19
    {
        public long Solve()
        {
            var input = File.ReadAllText(@"..\..\..\Input\Day19.txt");

            return RoundB(input);
        }

        private long RoundA(string input)
        {
            var blueprints = input.Split("\r\n").Select(ParseInput).ToArray();

            var result = blueprints.Select(x =>
            {
                (int Minute, int OreRobots, int Ore, int ClayRobots, int Clay, int ObsidianRobots, int Obsidian, int GeodeRobots, int Geodes) state = (0, 1, 0, 0, 0, 0, 0, 0, 0);
                var geode = Round(new Dictionary<long, int>(), x, state);
                return x.Id * geode;
            }).Sum();
            return result;
        }

        private long RoundB(string input)
        {
            var blueprints = input.Split("\r\n").Select(ParseInput).ToArray().Take(3);

            var result = blueprints.Select(x =>
            {
                (int Minute, int OreRobots, int Ore, int ClayRobots, int Clay, int ObsidianRobots, int Obsidian, int GeodeRobots, int Geodes) state = (0, 1, 0, 0, 0, 0, 0, 0, 0);
                var geode = Round(new Dictionary<long, int>(), x, state);
                return geode;
            }).ToArray();
            return result[0] * result[1] * result[2];
        }

        private int Round(Dictionary<long, int> cache, Blueprint blueprint, (int Minute, int OreRobots, int Ore, int ClayRobots, int Clay, int ObsidianRobots, int Obsidian, int GeodeRobots, int Geodes) state)
        {
            if (state.Minute == 32)
            {
                return state.Geodes;
            }

            var key = GetKey(state);
            if (cache.ContainsKey(key))
            {
                return cache.GetValueOrDefault(key);
            }

            var results = new List<int>();
            results.Add(Round(cache, blueprint, UpdateState(blueprint, (false, false, true), state)));
            results.Add(Round(cache, blueprint, UpdateState(blueprint, (false, true, false), state)));
            results.Add(Round(cache, blueprint, UpdateState(blueprint, (true, false, false), state)));
            results.Add(Round(cache, blueprint, UpdateState(blueprint, (false, false, false), state)));

            var result = results.Max();

            cache.Add(key, result);

            return result;
        }

        private long GetKey((int Minute, int OreRobots, int Ore, int ClayRobots, int Clay, int ObsidianRobots, int Obsidian, int GeodeRobots, int Geodes) state)
        {
            return long.Min(state.Minute, 40) +
                100 * long.Min(state.OreRobots, 40) +
                10000 * long.Min(state.Ore, 40) +
                1000000 * long.Min(state.ClayRobots, 40) +
                100000000 * long.Min(state.Clay, 40) +
                10000000000 * long.Min(state.ObsidianRobots, 40) +
                1000000000000 * long.Min(state.Obsidian, 40) +
                100000000000000 * long.Min(state.GeodeRobots, 40) +
                10000000000000000 * long.Min(state.Geodes, 40);
        }

        private (int Minute, int OreRobots, int Ore, int ClayRobots, int Clay, int ObsidianRobots, int Obsidian, int GeodeRobots, int Geodes) UpdateState(
            Blueprint blueprint,
            (bool ore, bool clay, bool obsidian) buildRobot,
            (int Minute, int OreRobots, int Ore, int ClayRobots, int Clay, int ObsidianRobots, int Obsidian, int GeodeRobots, int Geodes) state)
        {
            state.Minute += 1;
            (int ore, int clay, int obsidian, int geode) newRobot = (0, 0, 0, 0);

            if (blueprint.GeodeRobot.Ore <= state.Ore && blueprint.GeodeRobot.Obsidian <= state.Obsidian &&
                newRobot.geode != 1 && newRobot.obsidian != 1 && newRobot.clay != 1 && newRobot.ore != 1)
            {
                state.Ore -= blueprint.GeodeRobot.Ore;
                state.Obsidian -= blueprint.GeodeRobot.Obsidian;
                newRobot.geode = 1;
            }

            if (buildRobot.obsidian && blueprint.ObsidianRobot.Ore <= state.Ore && blueprint.ObsidianRobot.Clay <= state.Clay &&
                newRobot.geode != 1 && newRobot.obsidian != 1 && newRobot.clay != 1 && newRobot.ore != 1)
            {
                state.Ore -= blueprint.ObsidianRobot.Ore;
                state.Clay -= blueprint.ObsidianRobot.Clay;
                newRobot.obsidian = 1;
            }

            if (buildRobot.clay && blueprint.ClayRobot.Ore <= state.Ore &&
                newRobot.geode != 1 && newRobot.obsidian != 1 && newRobot.clay != 1 && newRobot.ore != 1)
            {
                state.Ore -= blueprint.ClayRobot.Ore;
                newRobot.clay = 1;
            }

            if (buildRobot.ore && blueprint.OreRobot.Ore <= state.Ore &&
                newRobot.geode != 1 && newRobot.obsidian != 1 && newRobot.clay != 1 && newRobot.ore != 1)
            {
                state.Ore -= blueprint.OreRobot.Ore;
                newRobot.ore = 1;
            }

            state.Ore += state.OreRobots;
            state.Clay += state.ClayRobots;
            state.Obsidian += state.ObsidianRobots;
            state.Geodes += state.GeodeRobots;

            state.OreRobots += newRobot.ore;
            state.ClayRobots += newRobot.clay;
            state.ObsidianRobots += newRobot.obsidian;
            state.GeodeRobots += newRobot.geode;

            return state;
        }

        private Blueprint ParseInput (string input)
        {
            var match = Regex.Match(input, @"Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian.");

            return new Blueprint
            {
                Id = int.Parse(match.Groups[1].Value),
                OreRobot = new OreOnly { Ore = int.Parse(match.Groups[2].Value) },
                ClayRobot = new OreOnly { Ore = int.Parse(match.Groups[3].Value) },
                ObsidianRobot = new OreClay
                {
                    Ore = int.Parse(match.Groups[4].Value),
                    Clay = int.Parse(match.Groups[5].Value),
                },
                GeodeRobot = new OreObsidian
                {
                    Ore = int.Parse(match.Groups[6].Value),
                    Obsidian = int.Parse(match.Groups[7].Value),
                },
            };
        }
    }

    struct Blueprint
    {
        public int Id { get; set; }
        public OreOnly OreRobot { get; set; }
        public OreOnly ClayRobot { get; set; }
        public OreClay ObsidianRobot { get; set; }
        public OreObsidian GeodeRobot { get; set; }
    }

    struct OreOnly
    {
        public int Ore { get; set; }
        public override string ToString()
        {
            return $"Ore: {Ore}";
        }
    }

    struct OreClay
    {
        public int Ore { get; set; }
        public int Clay { get; set; }
        public override string ToString()
        {
            return $"Ore: {Ore}, Clay: {Clay}";
        }
    }

    struct OreObsidian
    {
        public int Ore { get; set; }
        public int Obsidian { get; set; }
        public override string ToString()
        {
            return $"Ore: {Ore}, Obsidian: {Obsidian}";
        }
    }
}
