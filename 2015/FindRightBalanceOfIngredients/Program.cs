namespace FindRightBalanceOfIngredients
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ingredients = new BalancedIngredients();
            //int resultSum = 100;
            int numbers = 4;
            var partition = new int[numbers];

            for (int i = 0; i < numbers - 1; i++)
            {
                partition[i] = 1;                
            }

            partition[numbers - 1] = 97;
            //GeneratePartitions(partition);
            //ingredients.GeneratePermutations(partition, partition.Length);
            ingredients.GeneratePartitions(partition);

        }


    }

    public class BalancedIngredients
    {
        public List<int[]> EqualSumNumbers { get; }

        public BalancedIngredients()
        {
            EqualSumNumbers = new List<int[]>();
        }

        public void GeneratePartitions(int[] partition)
        {
            if (partition[partition.Length - 1] == 1)
            {
                foreach (int i in partition)
                {
                    Console.Write($"{i} ");
                }

                Console.WriteLine();
                return;
            }

            var changedPartition = partition;
            var lastNumber = changedPartition.Last();
            changedPartition[changedPartition.Length - 1] = lastNumber - 1;
            GeneratePermutations(partition, partition.Length);

            GeneratePartitions(changedPartition);
            //var changed = partition;
            //changed[changed.Length - 1]--;
            //GeneratePartitions(changed);


        }

        public void GeneratePermutations(int[] numbers, int size)
        {
            if (size == 1)
            {
                EqualSumNumbers.Add(numbers);
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
    }
}
