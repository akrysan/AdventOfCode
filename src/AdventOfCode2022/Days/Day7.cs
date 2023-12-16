namespace AdventOfCode2022.Days
{
    class Day7
    {
        public int Solve()
        {
            var input = File.ReadAllText(@"..\..\..\Input\Day7.txt");

            return RoundB(input);
        }

        private int RoundA(string input)
        {
            var root = Parse(input.Split("\r\n"));

            TreeTraversal(root);

            var stack = new Stack<Item>();
            stack.Push(root);

            var most = new List<Item>();
            while (stack.Count() != 0) {
                var d = stack.Pop();
                if (d.Size < 100000) {
                    most.Add(d);
                }
                foreach (var b in d.Children.Where(x => x.Type == 0))
                {
                    stack.Push(b);
                }

            }

            return most.Sum(x => x.Size);
        }

        void TreeTraversal(Item item) {
            foreach (var b in item.Children.Where(x => x.Type == 0))
            {
                TreeTraversal(b);
            }
            item.Size = item.Children.Sum(x => x.Size);
        }

        private int RoundB(string input)
        {
            var root = Parse(input.Split("\r\n"));

            TreeTraversal(root);

            var stack = new Stack<Item>();
            stack.Push(root);

            var most = new List<Item>();
            while (stack.Count() != 0)
            {
                var d = stack.Pop();
                if (d.Size >= 30000000-(70000000 - root.Size))
                {
                    most.Add(d);
                }
                foreach (var b in d.Children.Where(x => x.Type == 0))
                {
                    stack.Push(b);
                }

            }

            return most.OrderBy(x => x.Size).First().Size;
        }

        private Item Parse(string[] input2)
        {
            Item root = new Item();
            Item current = root;
            foreach (var line in input2)
            {
                if (line[0] == '$')
                {
                    var parts = line.Split(' ');
                    if (parts[1] == "cd")
                    {
                        if (parts[2] == "/")
                        {
                            current = root;
                        }
                        else if(parts[2] == "..") {
                            current = current.Parent;
                        }
                        else
                        {
                            var a = new Item();
                            a.Name = parts[2];
                            a.Parent = current;
                            a.Type = 0;

                            current.Children.Add(a);

                            current = a;
                        }
                    }
                    else if (parts[1] == "ls")
                    {
                    }
                }
                else if (line.StartsWith("dir"))
                {
                    //var parts = line.Split(" ");
                    //var a = 
                }
                else
                {
                    var parts = line.Split(" ");
                    var a = new Item();
                    a.Size = int.Parse(parts[0]);
                    a.Name = parts[1];
                    a.Type = 1;
                    current.Children.Add(a);
                }

            }

            return root;
        }

        public class Item
        {
            private List<Item> _children = new List<Item>();
            public int Size { get; set; }
            public int Type { get; set; }
            public string Name { get; set; }
            public List<Item> Children { get { return _children; } }
            public Item Parent { get; set; }
        }
    }
}
