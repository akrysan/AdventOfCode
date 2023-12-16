using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Numerics;
using System.Runtime;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day12
    {

        int minSteps = 1000000000;
        public long Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\input\day12.txt");

            return RoundC(input);
        }

        private long RoundA(string input)
        {
            var result = input.Split("\r\n");
            var map = new char[result[0].Length, result.Length];
            var path = new List<(int, int)>();
            var startPoint = (0, 0);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = result[j][i];
                    if (result[j][i] == 'S')
                    {
                        startPoint = (i, j);
                        map[i, j] = 'a';
                    }
                }
            }

            path.Add(startPoint);
            TreeClimbing(map, path, startPoint);

            return minSteps - 1;
        }

        void TreeClimbing(char[,] map, List<(int, int)> path, (int, int) current)
        {
            if (map[current.Item1, current.Item2] == 'E' && path.Count() < minSteps)
            {
                Console.WriteLine($"Min steps: {path.Count()}");
                minSteps = path.Count();
            }

            if (current.Item1 + 1 < map.GetLength(0))
            {
                var distance = (map[current.Item1 + 1, current.Item2] - map[current.Item1, current.Item2]);
                if (((distance == 0 || distance == 1) || (map[current.Item1, current.Item2] == 'z' && map[current.Item1 + 1, current.Item2] == 'E')) && !path.Contains((current.Item1 + 1, current.Item2)))
                {
                    path.Add((current.Item1 + 1, current.Item2));
                    TreeClimbing(map, path, (current.Item1 + 1, current.Item2));
                    path.Remove((current.Item1 + 1, current.Item2));
                }
            }
            if (0 <= current.Item1 - 1)
            {
                var distance = (map[current.Item1 - 1, current.Item2] - map[current.Item1, current.Item2]);
                if (((distance == 0 || distance == 1) || (map[current.Item1, current.Item2] == 'z' && map[current.Item1 - 1, current.Item2] == 'E')) && !path.Contains((current.Item1 - 1, current.Item2)))
                {
                    path.Add((current.Item1 - 1, current.Item2));
                    TreeClimbing(map, path, (current.Item1 - 1, current.Item2));
                    path.Remove((current.Item1 - 1, current.Item2));
                }
            }
            if (current.Item2 + 1 < map.GetLength(1))
            {
                var distance = (map[current.Item1, current.Item2 + 1] - map[current.Item1, current.Item2]);
                if (((distance == 0 || distance == 1) || (map[current.Item1, current.Item2] == 'z' && map[current.Item1, current.Item2 + 1] == 'E')) && !path.Contains((current.Item1, current.Item2 + 1)))
                {
                    path.Add((current.Item1, current.Item2 + 1));
                    TreeClimbing(map, path, (current.Item1, current.Item2 + 1));
                    path.Remove((current.Item1, current.Item2 + 1));
                }
            }
            if (0 <= current.Item2 - 1)
            {
                var distance = (map[current.Item1, current.Item2 - 1] - map[current.Item1, current.Item2]);
                if (((distance == 0 || distance == 1) || (map[current.Item1, current.Item2] == 'z' && map[current.Item1, current.Item2 - 1] == 'E')) && !path.Contains((current.Item1, current.Item2 - 1)))
                {
                    path.Add((current.Item1, current.Item2 - 1));
                    TreeClimbing(map, path, (current.Item1, current.Item2 - 1));
                    path.Remove((current.Item1, current.Item2 - 1));
                }
            }

        }

        private int RoundB(string input)
        {
            var result = input.Split("\r\n");
            var map = new char[result[0].Length, result.Length];
            var mapVisited = new int[result[0].Length, result.Length];
            var queue = new Queue<(int, int)>();
            var endPoint = (0, 0);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = result[j][i];
                    mapVisited[i, j] = 1000000;
                    if (result[j][i] == 'S')
                    {
                        map[i, j] = 'a';
                        mapVisited[i, j] = 0;
                        queue.Enqueue((i, j));
                    }
                    if (result[j][i] == 'E')
                    {
                        endPoint = (i, j);
                    }
                }
            }

            while (queue.Count() != 0)
            {
                var current = queue.Dequeue();

                foreach (var next in new (int, int)[] { (current.Item1 + 1, current.Item2),
                    (current.Item1, current.Item2 + 1),
                    (current.Item1, current.Item2 - 1),
                    (current.Item1 -1, current.Item2) })
                {
                    if (next.Item1 < 0 || map.GetLength(0) <= next.Item1 ||
                        next.Item2 < 0 || map.GetLength(1) <= next.Item2)
                    {
                        continue;
                    }

                    if ((map[next.Item1, next.Item2] - map[current.Item1, current.Item2]) <= 1 &&
                        mapVisited[current.Item1, current.Item2] + 1 < mapVisited[next.Item1, next.Item2])
                    {
                        mapVisited[next.Item1, next.Item2] = mapVisited[current.Item1, current.Item2] + 1;
                        queue.Enqueue((next.Item1, next.Item2));
                    }
                }
            }

            var result2 = new (int, int)[] { (endPoint.Item1 + 1, endPoint.Item2),
                    (endPoint.Item1, endPoint.Item2 + 1),
                    (endPoint.Item1, endPoint.Item2 - 1),
                    (endPoint.Item1 -1, endPoint.Item2) }
            .Where(x => map[x.Item1, x.Item2] == 'z')
            .Select(x => mapVisited[x.Item1, x.Item2])
            .Min() + 1;

            return result2;
        }

        private int RoundC(string input)
        {
            var result = input.Split("\r\n");
            var map = new char[result[0].Length, result.Length];
            var mapVisited = new int[result[0].Length, result.Length];
            var queue = new Queue<(int, int)>();
            var endPoint = (0, 0);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = result[j][i];
                    mapVisited[i, j] = 1000000;
                    if ((result[j][i] == 'S' || result[j][i] == 'a') && (i == 0 || j == 0 || i == map.GetLength(0) - 1 || j == map.GetLength(1) - 1))
                    {
                        map[i, j] = 'a';
                        mapVisited[i, j] = 0;
                        queue.Enqueue((i, j));
                    }
                    if (result[j][i] == 'E')
                    {
                        endPoint = (i, j);
                    }
                }
            }

            while (queue.Count() != 0)
            {
                var current = queue.Dequeue();

                foreach (var next in new (int, int)[] { (current.Item1 + 1, current.Item2),
                    (current.Item1, current.Item2 + 1),
                    (current.Item1, current.Item2 - 1),
                    (current.Item1 -1, current.Item2) })
                {
                    if (next.Item1 < 0 || map.GetLength(0) <= next.Item1 ||
                        next.Item2 < 0 || map.GetLength(1) <= next.Item2)
                    {
                        continue;
                    }

                    if ((map[next.Item1, next.Item2] - map[current.Item1, current.Item2]) <= 1 &&
                        mapVisited[current.Item1, current.Item2] + 1 < mapVisited[next.Item1, next.Item2])
                    {
                        mapVisited[next.Item1, next.Item2] = mapVisited[current.Item1, current.Item2] + 1;
                        queue.Enqueue((next.Item1, next.Item2));
                    }
                }
            }

            var result2 = new (int, int)[] { (endPoint.Item1 + 1, endPoint.Item2),
                    (endPoint.Item1, endPoint.Item2 + 1),
                    (endPoint.Item1, endPoint.Item2 - 1),
                    (endPoint.Item1 -1, endPoint.Item2) }
            .Where(x => map[x.Item1, x.Item2] == 'z')
            .Select(x => mapVisited[x.Item1, x.Item2])
            .Min() + 1;

            return result2;
        }
    }
}
