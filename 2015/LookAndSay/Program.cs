using System.Text;

namespace LookAndSay
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input = "1113122113";
            var game = new LookAndSay();
            var length = game.CalculateNumbers(input).Length;
            Console.WriteLine(length);
        }
    }

    public class LookAndSay
    {
        public string CalculateNumbers(string input)
        {
            var repetitions = 50;
            StringBuilder currentSequence;
            string lastSequence = input;

            for (int i = 0; i < repetitions; i++)
            {
                currentSequence = new StringBuilder();
                int length = lastSequence.Length;
                int occurrences = 0;

                for (int index = 0; index < length - 1; index++)
                {
                    var currentElement = int.Parse(lastSequence[index].ToString());
                    int nextElement = int.Parse(lastSequence[index + 1].ToString());

                    occurrences++;

                    if (currentElement != nextElement)
                    {
                        currentSequence.Append(occurrences);
                        currentSequence.Append(currentElement);

                        if (index + 1 == length - 1)
                        {
                            currentSequence.Append(1);
                            currentSequence.Append(nextElement);
                        }

                        occurrences = 0;
                    }

                    if (currentElement == nextElement && index + 1 == length - 1)
                    {
                        currentSequence.Append(occurrences + 1);
                        currentSequence.Append(currentElement);
                    }
                }

                lastSequence = currentSequence.ToString();
            }

            return lastSequence;
        }
    }
}
