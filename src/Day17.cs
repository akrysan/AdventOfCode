using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    class Day17
    {
        public long Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\input\day17.txt");

            return RoundB(input);
        }

        private long RoundA(string input)
        {
            var chamber = new List<string>();

            var rockNumber = 1;
            var round = 0;


            while (rockNumber < 2023)
            {
                var rock = NextRock(rockNumber);
                var position = (2, chamber.Count + 4);

                do
                {
                    position = (position.Item1, position.Item2 - 1);

                    var jet = NextJet(input, round);
                    round++;

                    position = MoveRock(chamber, rock, position, jet);
                }
                while (!RockInRestPosition(chamber, rock, position));

                MergeRock(chamber, rock, position);

                rockNumber++;
            }


            return chamber.Count;
        }

        private long RoundB(string input)
        {
            var chamber = new List<string>();

            var rockNumber = 1;
            var jetRound = 0;

            (int tall, int rockNumber, int jetRound) firstMatch = (0, 0, 0);
            (int tall, int rockNumber, int jetRound, int diffTall, int diffRockNumber, int diffJetRound) prevMatch = (0, 0, 0, 0, 0, 0);
            (int tall, int rockNumber, int jetRound, int diffTall, int diffRockNumber, int diffJetRound) roundMatch = (0, 0, 0, 0, 0, 0);

            while (rockNumber < 5000000)
            {
                var rock = NextRock(rockNumber);
                var position = (2, chamber.Count + 4);

                do
                {
                    position = (position.Item1, position.Item2 - 1);

                    var jet = NextJet(input, jetRound);
                    jetRound++;

                    position = MoveRock(chamber, rock, position, jet);
                }
                while (!RockInRestPosition(chamber, rock, position));

                MergeRock(chamber, rock, position);

                if (chamber[chamber.Count - 1].All(x => x == '#'))
                {
                    Console.WriteLine($"Top line occupied at tower tall {chamber.Count} dif {chamber.Count - prevMatch.tall} & rock numbers {rockNumber} dif {rockNumber - prevMatch.rockNumber}");

                    prevMatch = (chamber.Count, rockNumber, jetRound, chamber.Count - prevMatch.tall, rockNumber - prevMatch.rockNumber, jetRound - prevMatch.jetRound);

                    if (roundMatch.diffTall == prevMatch.diffTall)
                    {
                        roundMatch = (chamber.Count, rockNumber, jetRound, chamber.Count - roundMatch.tall, rockNumber - roundMatch.rockNumber, jetRound - roundMatch.jetRound);
                        break;
                    }

                    if (firstMatch.tall != 0 && roundMatch.tall == 0)
                    {
                        roundMatch = (chamber.Count, rockNumber, jetRound, chamber.Count - firstMatch.tall, rockNumber - firstMatch.rockNumber, jetRound - firstMatch.jetRound);
                    }

                    if (firstMatch.tall == 0)
                    {
                        firstMatch = (chamber.Count, rockNumber, jetRound);
                    }
                }

                rockNumber++;
            }

            var missRockNumber = ((BigInteger)1000000000000 - firstMatch.rockNumber) % roundMatch.diffRockNumber;
            var missRounds = ((BigInteger)1000000000000 - firstMatch.rockNumber) / roundMatch.diffRockNumber;

            rockNumber = 1;
            jetRound = 0;
            chamber = new List<string>();

            while (rockNumber <= missRockNumber)
            {
                var rock = NextRock(firstMatch.rockNumber + roundMatch.diffRockNumber + rockNumber);
                var position = (2, chamber.Count + 4);

                do
                {
                    position = (position.Item1, position.Item2 - 1);

                    var jet = NextJet(input, firstMatch.jetRound + roundMatch.diffJetRound + jetRound);
                    jetRound++;

                    position = MoveRock(chamber, rock, position, jet);
                }
                while (!RockInRestPosition(chamber, rock, position));

                MergeRock(chamber, rock, position);

                rockNumber++;
            }

            Console.WriteLine($"RoundB: tower tall {firstMatch.tall + chamber.Count + missRounds * roundMatch.diffTall}");
            return 0;
        }

        private string[] NextRock(int round)
        {
            switch (round % 5)
            {
                case 1:
                    return new string[] { "####" };
                case 2:
                    return new string[] { " # ", "###", " # " };
                case 3:
                    return new string[] { "  #", "  #", "###" };
                case 4:
                    return new string[] { "#", "#", "#", "#" };
                default:
                    return new string[] { "##", "##" };
            }
        }

        private char NextJet(string input, int round)
        {
            var offset = round % input.Length;
            return input[offset];
        }

        private (int, int) MoveRock(List<string> chamber, string[] rock, (int, int) position, char jet)
        {
            if ((jet == '>' && (position.Item1 + rock[0].Length) == 7) ||
                (jet == '<' && position.Item1 == 0))
            {
                return (position.Item1, position.Item2);
            }

            var result = (position.Item1 + 1, position.Item2);

            if (jet == '<')
            {
                result = (position.Item1 - 1, position.Item2);
            }

            for (int i = 0; (i < rock.Length) && (position.Item2 + i < chamber.Count); i++)
            {
                var line = rock[rock.Length - 1 - i];
                var rockX = 0;
                var chamberX = 0;

                if (jet == '<')
                {
                    for (int j = 0; j < line.Length; j++)
                    {
                        if (line[j] == '#')
                        {
                            rockX = j;
                            chamberX = position.Item1 - 1 + j;
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = line.Length - 1; 0 <= j; j--)
                    {
                        if (line[j] == '#')
                        {
                            rockX = j;
                            chamberX = position.Item1 + 1 + j;
                            break;
                        }
                    }
                }

                if (rock[rock.Length - 1 - i][rockX] == '#' && chamber[position.Item2 + i][chamberX] == '#')
                {
                    return (position.Item1, position.Item2);
                }
            }

            return result;
        }

        private bool RockInRestPosition(List<string> chamber, string[] rock, (int, int) position)
        {
            if (position.Item2 == 0)
            {
                return true;
            }

            for (var i = 0; i < rock[0].Length; i++)
            {
                var x = position.Item1 + i;
                var y = position.Item2;
                foreach (var line in rock.Reverse())
                {
                    if (line[i] == '#')
                    {
                        break;
                    }
                    else
                    {
                        y++;
                    }
                }

                if (chamber.Count < y)
                {
                    continue;
                }

                if (chamber[y - 1][x] == '#')
                {
                    return true;
                }
            }
            return false;
        }

        private void MergeRock(List<string> chamber, string[] rock, (int, int) position)
        {

            string MergeLine(string line1, string line2)
            {
                var result = line1.ToCharArray();

                for(var j = 0; j < line2.Length; j++)
                {
                    if (line2[j] == '#')
                    {
                        if (result[position.Item1 + j] == '#')
                        {
                            throw new InvalidOperationException("wrong merge");
                        }
                        result[position.Item1 + j] = line2[j];
                    }
                }
                return new string(result);
            }

            for (var i = 0; i < rock.Length; i++)
            {
                if (position.Item2 + i < chamber.Count)
                {
                    chamber[position.Item2 + i] = MergeLine(chamber[position.Item2 + i], rock[rock.Length - 1 - i]);
                }
                else
                {
                    chamber.Add(MergeLine("       ", rock[rock.Length - 1 - i]));
                }
            }
        }
    }
}
