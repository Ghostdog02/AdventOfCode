using System.Collections.Concurrent;

namespace FindRightBalanceOfIngredients
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ingredients = new BalancedIngredients();
            int resultSum = 100;
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

        //Generates all sequences which have a sum of 100
        //public void GeneratePartitions()
        //{
        //    for (int firstCoefficient = 1; firstCoefficient <= 99; firstCoefficient++)
        //    {
        //        for (int secondCoefficient = 1; secondCoefficient <= 99; secondCoefficient++)
        //        {
        //            for (int thirdCoefficient = 1; thirdCoefficient <= 99; thirdCoefficient++)
        //            {
        //                for (int fourthCoefficient = 1; fourthCoefficient <= 99; fourthCoefficient++)
        //                {
        //                    if ((firstCoefficient + secondCoefficient + thirdCoefficient + fourthCoefficient) == 100)
        //                    {
        //                        

        //                }
        //            }
        //        }
        //    }
        //}

        //private int CalculateScoreOfIngredient(int[] ingredientProperties, int coefficient)
        //{
        //    var tempScore = 0;

        //    foreach (int property in ingredientProperties)
        //    {
        //        tempScore += coefficient * property;
        //    }

        //    return tempScore;
        //}

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
            //if (partition[partition.Length - 1] == 1)
            //{
            //    foreach (int i in partition)
            //    {
            //        Console.Write($"{i} ");
            //    }

            //    Console.WriteLine();
            //    return;
            //}

            if (partition.Last() == 1)
            {
                CalculateCookieScore(partition);

                return;
            }

            if (CheckIfNumbersAreEqual(partition) && CheckTwoCollectionsForEquality(partition))
            {
                CalculateCookieScore(partition);
                GeneratedSequences.Add(partition);
                //int[] partitionCopy = new int[partition.Length];
                //Array.Copy(partition, partitionCopy, partition.Length);
                //partitionCopy[partitionCopy.Length - 1]--;
                //partitionCopy[partitionCopy.Length - 2]++;
                var partitionCopy = CopyAndAlterArray(partition, 0);
                GeneratePartitions(partitionCopy);
            }

            else if (CheckTwoCollectionsForEquality(partition))
            {
                CalculateCookieScore(partition);
                GeneratedSequences.Add(partition);
                if (partition.Last() == 2)
                {
                    int b = 0;
                }

                for (int i = 0; i < partition.Length - 1; i++)
                {
                    var partitionCopy = CopyAndAlterArray(partition, i);
                    
                    if (CheckTwoCollectionsForEquality(partitionCopy))
                    {
                        GeneratePartitions(partitionCopy);
                        GeneratePermutations(partitionCopy, partitionCopy.Length);

                        foreach (var currentPartition in CurrentPermutations)
                        {
                            if (CheckTwoCollectionsForEquality(currentPartition))
                                GeneratePartitions(currentPartition);
                        }
                    }
                    

                    //CurrentPermutations.Clear();
                }





                //foreach (int[] permutation in CurrentPermutations)
                //{

                //}

                //var changedPartition = partition;
                //var lastNumber = changedPartition.Last();
                //changedPartition[changedPartition.Length - 1] = lastNumber - 1;


                //GeneratePartitions(changedPartition);
                //var changed = partition;
                //changed[changed.Length - 1]--;
                //GeneratePartitions(changed);


            }




        }

        private void GeneratePermutations(int[] numbers, int size)
        {
            if (size == 1)
            {
                if (CheckTwoCollectionsForEquality(numbers))
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

            capacity = SetPropertyToZeroIfNegative(capacity);
            durability = SetPropertyToZeroIfNegative(durability);
            flavor = SetPropertyToZeroIfNegative(flavor);
            texture = SetPropertyToZeroIfNegative(texture);

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
            return !GeneratedSequences.Any(partition => partition.Intersect(numbers).Count() == numbers.Length);   
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
