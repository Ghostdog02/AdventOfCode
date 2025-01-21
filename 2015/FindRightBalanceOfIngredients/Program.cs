using System.Collections.Concurrent;

namespace FindRightBalanceOfIngredients
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ingredients = new BalancedIngredients();
            int numbers = 4;
            var partition = new int[numbers];

            for (int i = 0; i < numbers - 1; i++)
            {
                partition[i] = 1;
            }

            partition[numbers - 1] = 97;
            ////GeneratePartitions(partition);
            ////ingredients.GeneratePermutations(partition, partition.Length);
            ingredients.ReadInput();
            ingredients.GeneratePartitions(partition);
            Console.WriteLine(ingredients.TotalScore);

        }


    }

    public class BalancedIngredients
    {
        public List<int[]> CurrentPermutations { get; }

        public List<int[]> GeneratedSequences { get; }

        public List<int[]> Ingredients { get; }

        public int TotalScore { get; private set; }

        public BalancedIngredients()
        {
            CurrentPermutations = new List<int[]>();
            GeneratedSequences = new List<int[]>();
            Ingredients = new List<int[]>();
        }

        public void ReadInput()
        {
            var path = @"D:\Projects\AdventOfCode\2015\FindRightBalanceOfIngredients\Input.txt";
            using var reader = new StreamReader(path);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                int[] numbers = new int[5];

                var splitLine = line.Split(new string[] { " ", ", " }, StringSplitOptions.RemoveEmptyEntries);
                int index = 0;

                foreach (var property in splitLine)
                {
                    if (int.TryParse(property, out int digit))
                    {
                        numbers[index] = digit;
                        index++;
                    }
                }

                Ingredients.Add(numbers);
            }
        }

        //Generates all sequences which have a sum of 100
        public void GeneratePartitions(int[] partition)
        {
            if (partition.Last() == 1)
            {
                CalculateCookieScore(partition);

                return;
            }

            if (CheckIfNumbersAreEqual(partition) && !CheckTwoCollectionsForEquality(partition))
            {
                CalculateCookieScore(partition);
                GeneratedSequences.Add(partition);

                var partitionCopy = CopyAndAlterArray(partition, 0);
                GeneratePartitions(partitionCopy);
            }

            else if (!CheckTwoCollectionsForEquality(partition))
            {
                CalculateCookieScore(partition);
                GeneratedSequences.Add(partition);

                for (int i = 0; i < partition.Length - 1; i++)
                {
                    var partitionCopy = CopyAndAlterArray(partition, i);

                    if (!CheckTwoCollectionsForEquality(partitionCopy))
                    {
                        GeneratePartitions(partitionCopy);
                        GeneratePermutations(partitionCopy, partitionCopy.Length);

                        foreach (var currentPartition in CurrentPermutations)
                        {
                            if (!CheckTwoCollectionsForEquality(currentPartition))
                                GeneratePartitions(currentPartition);
                        }
                    }
                }
            }
        }

        private void GeneratePermutations(int[] numbers, int size)
        {
            if (size == 1)
            {
                if (!CheckTwoCollectionsForEquality(numbers))
                    CurrentPermutations.Add(numbers);

                return;
            }

            for (int i = 0; i < size; i++)
            {
                GeneratePermutations(numbers, size - 1);

                int swapIndex = (size % 2 == 1) ? 0 : i;

                Swap(swapIndex, size - 1, numbers);
            }
        }

        private void Swap(int firstIndex, int secondIndex, int[] numbers)
        {
            (numbers[secondIndex], numbers[firstIndex]) = (numbers[firstIndex], numbers[secondIndex]);
        }

        //It checks for all numbers without the last one
        private bool CheckIfNumbersAreEqual(int[] numbers)
        {
            bool areEqual = true;

            for (int i = 0; i < numbers.Length - 2; i++)
            {
                if (numbers[i] != numbers[i + 1])
                {
                    areEqual = false;
                    break;
                }
            }

            return areEqual;
        }

        private void CalculateCookieScore(int[] teaspoonsForEachIngredient)
        {
            int tempScore = 0;
            int capacity = 0;
            int durability = 0;
            int flavor = 0;
            int texture = 0;

            for (int i = 0; i < Ingredients.Count; i++)
            {
                capacity += (teaspoonsForEachIngredient[i] * Ingredients[i][0]);
                durability += (teaspoonsForEachIngredient[i] * Ingredients[i][1]);
                flavor += (teaspoonsForEachIngredient[i] * Ingredients[i][2]);
                texture += (teaspoonsForEachIngredient[i] * Ingredients[i][3]);
            }

            //capacity = SetPropertyToZeroIfNegative(capacity);
            //durability = SetPropertyToZeroIfNegative(durability);
            //flavor = SetPropertyToZeroIfNegative(flavor);
            //texture = SetPropertyToZeroIfNegative(texture);
            if (capacity == 0 || durability == 0 || flavor == 0 || texture == 0)
            {
                return;
            }

            tempScore = capacity * durability * flavor * texture;

            if (tempScore > TotalScore)
                TotalScore = tempScore;
        }

        private int SetPropertyToZeroIfNegative(int property)
        {
            return property < 0 ? 0 : property;
        }

        private bool CheckTwoCollectionsForEquality(int[] numbers)
        {
            bool areEqual = false;

            if (GeneratedSequences.Count != 0)
            {
                foreach (int[] sequence in GeneratedSequences)
                {
                    areEqual = true;
                    for (int i = 0; i < sequence.Length; i++)
                    {
                        if (numbers[i] != sequence[i])
                        {
                            areEqual = false;
                        }
                    }

                    if (areEqual)
                        break;
                }
            }

            return areEqual;
            //return !GeneratedSequences.Any(partition => partition.Intersect(numbers).Count() == numbers.Length);   
        }

        private int[] CopyAndAlterArray(int[] partition, int i)
        {
            int[] partitionCopy = new int[partition.Length];
            Array.Copy(partition, partitionCopy, partition.Length);
            partitionCopy[partitionCopy.Length - 1]--;
            partitionCopy[partitionCopy.Length - i - 2]++;

            return partitionCopy;
        }

    }
}
