using System.Collections.Concurrent;

namespace FindRightBalanceOfIngredients
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ingredients = new BalancedIngredients();
            ingredients.ReadInput();
            ingredients.FindBestRecipe();
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

        public void FindBestRecipe()
        {
            for (int i = 1; i <= 96; i++)
            {
                for (int j = 1; j + i <= 98; j++)
                {
                    for (int l = 1; l + j + i <= 99; l++)
                    {
                        int k = 100 - (l + j + i);
                        CalculateCookieScore(new int[] { i, j, l, k });
                    }
                }
            }
        }

        private void CalculateCookieScore(int[] teaspoonsForEachIngredient)
        {
            int tempScore = 0;

            int capacity = 0;
            int durability = 0;
            int flavor = 0;
            int texture = 0;
            //Added variable for part 2
            int calories = 0;

            for (int i = 0; i < Ingredients.Count; i++)
            {
                capacity += (teaspoonsForEachIngredient[i] * Ingredients[i][0]);
                durability += (teaspoonsForEachIngredient[i] * Ingredients[i][1]);
                flavor += (teaspoonsForEachIngredient[i] * Ingredients[i][2]);
                texture += (teaspoonsForEachIngredient[i] * Ingredients[i][3]);
                calories += (teaspoonsForEachIngredient[i] * Ingredients[i][4]);
            }

            if ((capacity < 0 || durability < 0 || flavor < 0 || texture < 0) || calories != 500)
            {
                return;
            }

            tempScore = capacity * durability * flavor * texture;

            if (tempScore > TotalScore)
                TotalScore = tempScore;
        }
    }
}
