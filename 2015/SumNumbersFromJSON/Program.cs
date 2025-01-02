using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SumNumbersFromJSON
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText(@"D:\Projects\AdventOfCode\2015\Exercises\AccountingSoftware\Input.txt");
            var accounting = new SumNumbers();
            JObject o = JObject.Parse(input);
            var sum = accounting.AddAllNumbers(o);
            Console.WriteLine(sum);
        }

        //Task 1
        //public static string[] ReadInput()
        //{
        //    var inputPath = @"D:\Projects\AdventOfCode\2015\Exercises\AccountingSoftware\Input.txt";
        //    string line;
        //    using var reader = new StreamReader(inputPath);
        //    //var lines = new List<string>();
        //    line = reader.ReadToEnd();
        //    var separators = new char[] { '{', '}', '[', ']', ',', ':','"' };

        //    var lines = input[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
        //    return lines;
        //}
    }

    public class SumNumbers
    {
        //Task 1
        //    //public int AddAllNumbers(string[] input)
        //    //{
        //    //    int sum = 0;
        //    //    foreach (var input[i] in input)
        //    //    {
        //    //        var number = 0;

        //    //        if (int.TryParse(input[i], out number))
        //    //        {
        //    //            sum += number;
        //    //        }

        //    //        //Console.WriteLine(input[i]);
        //    //    }

        //    //    return sum;
        //    //}
        public int AddAllNumbers(JObject current)
        {
            int[] sum = new int[1];

            foreach (var token in current)
            {
                var doesContainRed = token.Value.Children().Values().Any(a => a.ToString().Equals("red") && a.Parent.Type != JTokenType.Array);
                if (!doesContainRed)
                    TraverseThroughJson(token.Value, sum);
                else
                    break;
            }



            return sum[0];
        }

        private void TraverseThroughJson(JToken current, int[] sum)
        {
            if (!current.Children().Any())
            {
                if (int.TryParse(current.ToString(), out int number))
                    sum[0] += number;
                return;
            }

            foreach (var token in current.Children())
            {
                var doesContainRed = token.Children().Values().Any(a => a.ToString().Equals("red") && a.Parent.Type != JTokenType.Array);

                if (!doesContainRed)
                {
                    TraverseThroughJson(token, sum);
                }


            }
        }
    }
}