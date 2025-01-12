using System.Collections.Generic;
using System.IO;
using System;

namespace OptimalSeatingArrangement
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\Projects\AdventOfCode\2015\OptimalSeatingArrangement\Input2.txt";
            var arrangement = new FindOptimalArrangement();
            arrangement.ProcessInput(path);
            arrangement.FindOptimalSeatingArrangement();
            Console.WriteLine(arrangement.TotalHappiness);
        }


    }

    public class FindOptimalArrangement
    {
        public Dictionary<(string, string), int> PeopleWithHappiness { get; }

        public List<string> Names { get; }

        public int TotalHappiness { get; private set; }

        public FindOptimalArrangement()
        {
            PeopleWithHappiness = new Dictionary<(string, string), int>();
            Names = new List<string>();
            TotalHappiness = 0;
        }

        public void ProcessInput(string path)
        {
            string line;
            using var reader = new StreamReader(path);
            //var lines = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                //1st and last element - names
                var words = line.Split(' ');
                var firstName = words[0];
                var lastName = words[words.Length - 1].Trim('.');
                var operation = words[2];
                var points = words[3];

                if (!Names.Contains(firstName))
                    Names.Add(firstName);

                if (!Names.Contains(lastName))
                    Names.Add(lastName);

                switch (operation)
                {
                    case "lose":
                        //Add two people's names as key and reverse the sign of the number as the operation is lose
                        PeopleWithHappiness.Add((firstName, lastName), int.Parse(points.Insert(0, "-")));
                        break;

                    case "gain":
                        PeopleWithHappiness.Add((firstName, lastName), int.Parse(points));
                        break;

                    default:
                        break;
                }
            }
        }

        public void FindOptimalSeatingArrangement()
        {
            GenerateAllPermutationsOfNames(Names.Count);
        }

        private void GenerateAllPermutationsOfNames(int currentIndex)
        {
            if (currentIndex == 1)
            {
                CalculateTotalHappiness();
                //foreach (var name in Names)
                //{
                //    Console.Write($"{name} ");
                //}
                //Console.WriteLine();
                return;
            }

            else
            {
                GenerateAllPermutationsOfNames(currentIndex - 1);

                for (int i = currentIndex; i >= 0; i--)
                {
                    if (currentIndex % 2 == 0)
                    {
                        Swap(i, currentIndex - 1);
                    }

                    else
                    {
                        Swap(0, currentIndex - 1);

                    }

                    GenerateAllPermutationsOfNames(currentIndex - 1);
                }

            }
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = Names[firstIndex];
            Names[firstIndex] = Names[secondIndex];
            Names[secondIndex] = temp;
        }

        private void CalculateTotalHappiness()
        {
            var tempHappiness = 0;

            for (int i = 0; i < Names.Count; i++)
            {
                var currentName = Names[i];

                if (i == 0)
                {
                    tempHappiness = AddCurrentHappiness(Names[Names.Count - 1], currentName, Names[i + 1], tempHappiness);
                }

                else if (i == Names.Count - 1)
                {
                    tempHappiness = AddCurrentHappiness(Names[i - 1], currentName, Names[0], tempHappiness);
                }

                else
                {
                    tempHappiness = AddCurrentHappiness(Names[i - 1], currentName, Names[i + 1], tempHappiness);
                }
            }

            if (tempHappiness > TotalHappiness)
            {
                TotalHappiness = tempHappiness;
            }
        }

        private int AddCurrentHappiness(string previousName, string currentName, string nextName, int tempHappiness)
        {
            tempHappiness += PeopleWithHappiness[(currentName, nextName)];
            tempHappiness += PeopleWithHappiness[(currentName, previousName)];

            return tempHappiness;
        }
    }
}