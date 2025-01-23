using System.Collections;

namespace MakeCombinationsOfTheVolumeOfContainers
{
    public class Program
    {
        static void Main(string[] args)
        {
            var creator = new CombinationsCreator();
            creator.ReadInput();
            var combinations = new List<int>();
            creator.MakeCombinations(combinations, 0);
            //Console.WriteLine(creator.NumberOfCombinations);
            Console.WriteLine(creator.CalculateMinimalNumberOfCombinations()); 
        }
    }

    public class CombinationsCreator
    {
        //private int[] VolumeOfContainers = { 43, 3, 4, 10, 21, 44, 4, 6, 47, 31, 34, 17, 17, 44, 36, 31, 46, 9, 27, 38 };

        public List<int> VolumeOfContainers { get; private set; }

        public List<List<int>> Combinations { get; private set; }

        public int NumberOfCombinations { get; private set; }

        public CombinationsCreator()
        {
            VolumeOfContainers = new List<int>();
            Combinations = new List<List<int>>();
        }

        //Part 1
        //public void MakeCombinations(List<int> currentCombinations, int currentIndex)
        //{
        //    if (currentIndex == VolumeOfContainers.Count)
        //    {
        //        if (currentCombinations.Sum() == 150)
        //            NumberOfCombinations++;

        //        return;
        //    }

        //    currentCombinations.Add(VolumeOfContainers[currentIndex]);
        //    MakeCombinations(currentCombinations, currentIndex + 1);

        //    currentCombinations.Remove(VolumeOfContainers[currentIndex]);
        //    MakeCombinations(currentCombinations, currentIndex + 1);
        //}

        //Part 2
        public void MakeCombinations(List<int> currentCombinations, int currentIndex)
        {
            if (currentIndex == VolumeOfContainers.Count)
            {
                if (currentCombinations.Sum() == 150)
                {
                    var copy = new List<int>(currentCombinations);
                    Combinations.Add(copy);
                }

                return;
            }

            currentCombinations.Add(VolumeOfContainers[currentIndex]);
            MakeCombinations(currentCombinations, currentIndex + 1);

            currentCombinations.Remove(VolumeOfContainers[currentIndex]);
            MakeCombinations(currentCombinations, currentIndex + 1);
        }

        public void ReadInput()
        {
            var path = @"D:\Projects\AdventOfCode\2015\MakeCombinationsOfTheVolumeOfContainers\Input.txt";
            using var reader = new StreamReader(path);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                var splitLine = line.Split(new string[] { " ", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                VolumeOfContainers.Add(int.Parse(splitLine[0]));
            }
        }

        public int CalculateMinimalNumberOfCombinations()
        {
            var minimalCount = Combinations.Min(a => a.Count);
            return Combinations.Count(a => a.Count == minimalCount);
        }
    }
}
