namespace FindAuntSue
{
    public class Program
    {
        static void Main(string[] args)
        {
            var finder = new AuntSueFinder();
            finder.ReadInput();
            finder.FindRightAunt();
        }
    }

    public class AuntSueFinder
    {
        public readonly List<(int, Dictionary<string, int>)> Compounds;

        //Part 1
        //Dictionary<string, int> GivenCompound = new Dictionary<string, int>
        //{
        //    {"children", 3 },
        //    {"cats", 7},
        //    {"samoyeds", 2},
        //    {"pomeranians", 3},
        //    {"akitas", 0},
        //    {"vizslas", 0},
        //    {"goldfish", 5},
        //    {"trees", 3},
        //    {"cars", 2},
        //    {"perfumes", 1}
        //};

        //Part 2
        Dictionary<string, int> GivenCompound = new Dictionary<string, int>
        {
            {"children", 3 },
            {"cats", 14},
            {"samoyeds", 2},
            {"pomeranians", 0},
            {"akitas", 0},
            {"vizslas", 0},
            {"goldfish", 0},
            {"trees", 6},
            {"cars", 2},
            {"perfumes", 1}
        };

        public AuntSueFinder()
        {
            Compounds = new List<(int auntNumber, Dictionary<string, int> description)>();
        }

        public void ReadInput()
        {
            var path = @"D:\Projects\AdventOfCode\2015\FindAuntSue\Input.txt";
            using var reader = new StreamReader(path);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                var splitLine = line.Split(new string[] { " ", ", ", ": " }, StringSplitOptions.RemoveEmptyEntries);
                var currentCompound = (int.Parse(splitLine[1]), new Dictionary<string, int>());

                var tempDictionary = new Dictionary<string, int>();

                for (int i = 2; i < splitLine.Length - 1; i += 2)
                {
                    tempDictionary.Add(splitLine[i], int.Parse(splitLine[i + 1]));
                }

                currentCompound.Item2 = tempDictionary;
                Compounds.Add(currentCompound);

            }
        }

        public void FindRightAunt()
        {
            foreach (var compound in Compounds)
            {
                //Checks If Compounds contains current compound
                bool doesContainDescription = true;

                foreach (string key in compound.Item2.Keys)
                {
                    if (GivenCompound[key] != compound.Item2[key])
                    {
                        doesContainDescription = false;
                        break;
                    }
                }

                if (doesContainDescription)
                {
                    //Filter the given compound to contain only values that do not appear in the current compound
                    var remainingDescription = GivenCompound.Where(a => !compound.Item2.ContainsValue(a.Value));

                    //If it does not contain zero it means that the right aunt is found
                    if (!remainingDescription.Any(a => a.Value == 0))
                    {
                        foreach (var value in remainingDescription)
                        {
                            Console.WriteLine(value.Value);
                        }
                        Console.WriteLine(compound.Item1);
                        break;
                    }

                }
            }
        }
    }
}
