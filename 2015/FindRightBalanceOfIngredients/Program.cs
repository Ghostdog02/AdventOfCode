namespace FindRightBalanceOfIngredients
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ingredients = new BalancedIngredients();
            //int resultSum = 100;
            //int numbers = 4;
            //var partition = new int[numbers];

            //for (int i = 0; i < numbers - 1; i++)
            //{
            //    partition[i] = 1;
            //}

            //partition[numbers - 1] = 97;
            ////GeneratePartitions(partition);
            ////ingredients.GeneratePermutations(partition, partition.Length);
            ingredients.ReadInput();
            ingredients.GeneratePartitions();
            
        }


    }

    public class BalancedIngredients
    {
        //public List<int[]> CurrentPermutations { get; }

        //public List<int[]> GeneratedSequences { get; }

        public List<int[]> Ingredients { get; }

        public int TotalScore { get; private set; }

        public BalancedIngredients()
        {
            //CurrentPermutations = new List<int[]>();
            //GeneratedSequences = new List<int[]>();
            Ingredients = new List<int[]>();
        }

        //Generates all sequences which have a sum of 100
        public void GeneratePartitions()
        {
            for (int firstCoefficient = 1; firstCoefficient <= 99; firstCoefficient++)
            {
                for (int secondCoefficient = 1; secondCoefficient <= 99; secondCoefficient++)
                {
                    for (int thirdCoefficient = 1; thirdCoefficient <= 99; thirdCoefficient++)
                    {
                        for (int fourthCoefficient = 1; fourthCoefficient <= 99; fourthCoefficient++)
                        {
                            if ((firstCoefficient + secondCoefficient + thirdCoefficient + fourthCoefficient) == 100)
                            {
                                var tempScore = 0;

                                tempScore += CalculateScoreOfIngredient(Ingredients[0], firstCoefficient);
                                tempScore += CalculateScoreOfIngredient(Ingredients[1], secondCoefficient);
                                tempScore += CalculateScoreOfIngredient(Ingredients[2], thirdCoefficient);
                                tempScore += CalculateScoreOfIngredient(Ingredients[3], fourthCoefficient);

                                if (tempScore > TotalScore)
                                    TotalScore = tempScore;
                            }

                        }
                    }
                }
            }
        }

        private int CalculateScoreOfIngredient(int[] ingredientProperties, int coefficient)
        {
            var tempScore = 0;

            foreach (int property in ingredientProperties)
            {
                tempScore += coefficient * property;
            }

            return tempScore;
        }

        public void ReadInput()
        {
            var path = @"D:\Projects\AdventOfCode\2015\FindRightBalanceOfIngredients\Input.txt";
            using var reader = new StreamReader(path);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                int[] numbers = new int[5];
                
                var splitLine = line.Split(' ');
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

        //public void GeneratePartitions(int[] partition)
        //{
        //    //if (partition[partition.Length - 1] == 1)
        //    //{
        //    //    foreach (int i in partition)
        //    //    {
        //    //        Console.Write($"{i} ");
        //    //    }

        //    //    Console.WriteLine();
        //    //    return;
        //    //}

        //    if (CheckIfNumbersAreEqual(partition) && !GeneratedSequences.Contains(partition))
        //    {
        //        partition[partition.Length - 1]--;
        //        partition[partition.Length - 2]++;
        //        GeneratedSequences.Add(partition);
        //        GeneratePartitions(partition);
        //    }

        //    else
        //    {
        //        GeneratePermutations(partition, partition.Length);

        //        foreach (int[] permutation in CurrentPermutations)
        //        {

        //        }

        //        var changedPartition = partition;
        //        var lastNumber = changedPartition.Last();
        //        changedPartition[changedPartition.Length - 1] = lastNumber - 1;


        //        GeneratePartitions(changedPartition);
        //        //var changed = partition;
        //        //changed[changed.Length - 1]--;
        //        //GeneratePartitions(changed);
        //    }




        //}

        //public void GeneratePermutations(int[] numbers, int size)
        //{
        //    if (size == 1)
        //    {
        //        CurrentPermutations.Add(numbers);
        //        return;
        //    }

        //    for (int i = 0; i < size; i++)
        //    {
        //        GeneratePermutations(numbers, size - 1);

        //        int swapIndex = (size % 2 == 1) ? 0 : i;

        //        Swap(swapIndex, size - 1, numbers);
        //    }
        //}

        //private void Swap(int firstIndex, int secondIndex, int[] numbers)
        //{
        //    (numbers[secondIndex], numbers[firstIndex]) = (numbers[firstIndex], numbers[secondIndex]);
        //}

        //private bool CheckIfNumbersAreEqual(int[] numbers)
        //{
        //    bool areEqual = true;

        //    //It checks for all without the last one
        //    for (int i = 0; i < numbers.Length - 1; i++)
        //    {
        //        if (numbers[i] != numbers[i + 1])
        //        {
        //            areEqual = false; 
        //            break;
        //        }
        //    }

        //    return areEqual;
        //}
    }
}
