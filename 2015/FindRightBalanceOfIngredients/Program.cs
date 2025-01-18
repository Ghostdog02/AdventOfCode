namespace FindRightBalanceOfIngredients
{
    public class Program
    {
        //public List<int> numbers = new List<int>();

        public static void Main(string[] args)
        {
            var ingredients = new BalancedIngredients();
            //int resultSum = 100;
            int numbers = 3;
            int[] partition = [1, 2, 3];
            //for (int i = 0; i < numbers - 1; i++)
            //{
            //    partition[i] = 1;
            //}

            //partition[numbers - 1] = resultSum - numbers;

            //for (int i = 0; i < numbers; i++)
            //{
            //    partition[i] = i + 1;
            //}
            //GeneratePartitions(partition);
            ingredients.GeneratePermutations(partition, partition.Length);
        }


    }

    public class BalancedIngredients
    {
        public void GeneratePartitions(int[] partition)
        {
            //var changed = partition;
            //changed[changed.Length - 1]--;
            //GeneratePartitions(changed);
            GeneratePermutations(partition, partition.Length);
            //TO DO: Implement Heap's Algorithm and make an base case for recursion
        }

        public void GeneratePermutations(int[] numbers, int size)
        {
            if (size == 1)
            {
                foreach (var number in numbers)
                {
                    Console.Write($"{number} ");
                }

                Console.WriteLine();

                return;
            }

            for (int i = 0; i < size; i++)
            {
                GeneratePermutations(numbers, size - 1);

                int swapIndex = (size % 2 == 1) ? 0 : i;

                Swap(swapIndex, size-1, numbers);
            }
        }

        private void Swap(int firstIndex, int secondIndex, int[] numbers)
        {
            (numbers[secondIndex], numbers[firstIndex]) = (numbers[firstIndex], numbers[secondIndex]);
        }
    }
}
