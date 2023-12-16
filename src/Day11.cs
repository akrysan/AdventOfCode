using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode
{
    class Day11
    {
        public long Solve()
        {
            var input = System.IO.File.ReadAllText(@"..\..\..\input\day11.txt");

            return RoundB(input);
        }

        //private long RoundA(string input)
        //{
        //    var result = input.Split("\r\n\r\n");

        //    var monkeys = new List<Monkey>();

        //    foreach (var monkeyInput in result) {
        //        var monkerProp = monkeyInput.Split("\r\n");
        //        var monkey = new Monkey();
        //        monkey.Index = int.Parse(monkerProp[0].Split(":")[0].Split(" ")[1]);
        //        monkey.Items = monkerProp[1].Split(": ")[1].Split(", ").Select(x => BigInteger.Parse(x)).ToList();
        //        monkey.Operation = monkerProp[2].Split(": ")[1].Split("= ")[1];
        //        monkey.Test = int.Parse(monkerProp[3].Split(": ")[1].Split("by ")[1]);
        //        monkey.TestTrue = int.Parse(monkerProp[4].Split(": ")[1].Split("to monkey ")[1]);
        //        monkey.TestFalse = int.Parse(monkerProp[5].Split(": ")[1].Split("to monkey ")[1]);

        //        monkeys.Add(monkey);
        //    }

        //    var monkeysInspected = new BigInteger[monkeys.Count()];

        //    for (int i = 1; i <= 10000; i++)
        //    {
        //        foreach (var monkey in monkeys)
        //        {
        //            foreach (var item in monkey.Items) {
        //                var newItem = (BigInteger)0;
        //                if (monkey.Operation[4] == '*')
        //                {
        //                    int.TryParse(monkey.Operation.Split("* ")[1], out var modifier);
        //                    var longModifier = (BigInteger)modifier;
        //                    if (longModifier == 0)
        //                    {
        //                        longModifier = item;
        //                    }
        //                    newItem = item * longModifier;
        //                }
        //                else if (monkey.Operation[4] == '+')
        //                {
        //                    int.TryParse(monkey.Operation.Split("+ ")[1], out var modifier);
        //                    var longModifier = (BigInteger)modifier;
        //                    if (longModifier == 0)
        //                    {
        //                        longModifier = item;
        //                    }
        //                    newItem = item + longModifier;
        //                }
        //                else {
        //                    throw new InvalidOperationException(monkey.Operation);
        //                }

        //                //newItem = (int)(newItem / 3);

        //                if ((newItem % monkey.Test) == 0)
        //                {
        //                    monkeys[monkey.TestTrue].Items.Add(newItem);
        //                }
        //                else {
        //                    monkeys[monkey.TestFalse].Items.Add(newItem);
        //                }
        //            }

        //            monkeysInspected[monkey.Index] = monkeysInspected[monkey.Index] + monkey.Items.Count();
        //            monkey.Items = new List<BigInteger>();
        //        }
        //    }

        //    monkeysInspected = monkeysInspected.OrderDescending().ToArray();

        //    Console.WriteLine(monkeysInspected[0] * monkeysInspected[1]);

        //    return -1;
        //}

        private int RoundB(string input)
        {
            var result = input.Split("\r\n\r\n");

            var monkeys = new List<Monkey>();
            ;
            var k = 0;
            foreach (var monkeyInput in result)
            {
                var monkerProp = monkeyInput.Split("\r\n");
                var monkey = new Monkey();
                monkey.Index = int.Parse(monkerProp[0].Split(":")[0].Split(" ")[1]);
                monkey.Items = monkerProp[1].Split(": ")[1].Split(", ").Select(x =>
                {
                    var item = new Item
                    {
                        WorryLevel = BigInteger.Parse(x),
                        InitialValue = BigInteger.Parse(x)
                    };
                    item.VisitedMonkeys.Add(k);
                    return item;
                }).ToList();
                monkey.Operation = monkerProp[2].Split(": ")[1].Split("= ")[1];
                monkey.Test = int.Parse(monkerProp[3].Split(": ")[1].Split("by ")[1]);
                monkey.TestTrue = int.Parse(monkerProp[4].Split(": ")[1].Split("to monkey ")[1]);
                monkey.TestFalse = int.Parse(monkerProp[5].Split(": ")[1].Split("to monkey ")[1]);

                monkeys.Add(monkey);
                k++;
            }

            var monkeysInspected = new BigInteger[monkeys.Count()];

            for (int i = 1; i <= 10000; i++)
            {
                Console.Write($"Round {i}: ");
                foreach (var monkey in monkeys)
                {
                    foreach (var item in monkey.Items)
                    {
                        if (monkey.Operation[4] == '*')
                        {
                            int.TryParse(monkey.Operation.Split("* ")[1], out var modifier);
                            var longModifier = (BigInteger)modifier;
                            if (longModifier == 0)
                            {
                                longModifier = item.WorryLevel;
                            }
                            item.WorryLevel = item.WorryLevel * longModifier;
                        }
                        else if (monkey.Operation[4] == '+')
                        {
                            int.TryParse(monkey.Operation.Split("+ ")[1], out var modifier);
                            var longModifier = (BigInteger)modifier;
                            if (longModifier == 0)
                            {
                                longModifier = item.WorryLevel;
                            }
                            item.WorryLevel = item.WorryLevel + longModifier;
                        }
                        else
                        {
                            throw new InvalidOperationException(monkey.Operation);
                        }

                        //item.WorryLevel = (int)(item.WorryLevel / 3);

                        if (item.WorryLevel > 19 * 3 * 11 * 17 * 5 * 2 * 13 * 7)
                        {
                            item.WorryLevel = item.WorryLevel % (19 * 3 * 11 * 17 * 5 * 2 * 13 * 7);
                        }

                        if ((item.WorryLevel % monkey.Test) == 0)
                        {
                            item.VisitedMonkeys.Add(monkey.TestTrue);
                            monkeys[monkey.TestTrue].Items.Add(item);
                        }
                        else
                        {
                            item.VisitedMonkeys.Add(monkey.TestFalse);
                            monkeys[monkey.TestFalse].Items.Add(item);
                        }
                    }

                    Console.Write($"Monkey {monkey.Index} -> {monkey.Items.Count()} ");
                    monkeysInspected[monkey.Index] = monkeysInspected[monkey.Index] + monkey.Items.Count();
                    monkey.Items = new List<Item>();
                }

                Console.WriteLine("");
            }

            monkeysInspected = monkeysInspected.OrderDescending().ToArray();

            Console.WriteLine(monkeysInspected[0] * monkeysInspected[1]);

            return -1;
        }

    }
    public class Monkey
    {
        public int Index { get; set; }
        public List<Item> Items { get; set; }
        public string Operation { get; set; }
        public int Test { get; set; }
        public int TestTrue { get; set; }
        public int TestFalse { get; set; }

    }

    public class Item {
        public int Index { get; set; }
        public BigInteger InitialValue { get; set; }
        public BigInteger WorryLevel { get; set; }
        public List<int> VisitedMonkeys { get; set; } = new List<int>();
    }
}
