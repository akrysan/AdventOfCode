namespace AdventOfCode2022.Days
{
    class Day2
    {
        public int Solve()
        {
            var input = File.ReadAllText(@"..\..\..\Input\Day2.txt");

            return RoundB(input);
        }

        private int RoundA(string input) {
            var result = input
                .Split("\r\n")
                .Select(x => x.Split(" "))
                .Select(Score)
                .Sum();

            return result;
        }

        private int RoundB(string input)
        {
            var result = input
                .Split("\r\n")
                .Select(x => x.Split(" "))
                .Select(MapStrategy)
                .Select(Score)
                .Sum();

            return result;
        }

        private int Score(string[] game) {
            var score = 0;
            if (game[0] == "A" && game[1] == "X")
            {
                score = 3;
            }
            else if (game[0] == "A" && game[1] == "Y")
            {
                score = 6;
            }
            else if (game[0] == "A" && game[1] == "Z")
            {
                score = 0;
            }
            else if (game[0] == "B" && game[1] == "X")
            {
                score = 0;
            }
            else if (game[0] == "B" && game[1] == "Y")
            {
                score = 3;
            }
            else if (game[0] == "B" && game[1] == "Z")
            {
                score = 6;
            }
            else if (game[0] == "C" && game[1] == "X")
            {
                score = 6;
            }
            else if (game[0] == "C" && game[1] == "Y")
            {
                score = 0;
            }
            else if (game[0] == "C" && game[1] == "Z")
            {
                score = 3;
            }

            if (game[1] == "X")
            {
                score += 1;
            }
            else if (game[1] == "Y")
            {
                score += 2;
            }
            else if (game[1] == "Z")
            {
                score += 3;
            }

            return score;
        }

        private string[] MapStrategy(string[] game)
        {
            if (game[0] == "A" && game[1] == "X")
            {
                game[1] = "Z";
            }
            else if (game[0] == "A" && game[1] == "Y")
            {
                game[1] = "X";
            }
            else if (game[0] == "A" && game[1] == "Z")
            {
                game[1] = "Y";
            }
            else if (game[0] == "B" && game[1] == "X")
            {
                game[1] = "X";
            }
            else if (game[0] == "B" && game[1] == "Y")
            {
                game[1] = "Y";
            }
            else if (game[0] == "B" && game[1] == "Z")
            {
                game[1] = "Z";
            }
            else if (game[0] == "C" && game[1] == "X")
            {
                game[1] = "Y";
            }
            else if (game[0] == "C" && game[1] == "Y")
            {
                game[1] = "Z";
            }
            else if (game[0] == "C" && game[1] == "Z")
            {
                game[1] = "X";
            }

            return game;
        }
    }
}
