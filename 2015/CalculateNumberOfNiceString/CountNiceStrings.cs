using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;

namespace CalculateNumberOfNiceStrings
{
    public class CountNiceStrings
    {
        public static void Main(string[] args)
        {
            var countNiceStrings = new CountNiceStrings();
            var input = countNiceStrings.ReadInput();
            var niceStrings = countNiceStrings.ProcessInputTwo(input);
            Console.WriteLine(niceStrings);
        }

        public List<string> ReadInput()
        {
            var path = @"D:\Projects\AdventOfCode\2015\Exercises\CalculateNumberOfNiceString\Input.txt";
            string line;
            using var reader = new StreamReader(path);
            var lines = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line.Trim());
            }



            return lines;
        }

        public int ProcessInputTwo(List<string> input)
        {
            int counter = 0;

            foreach (var line in input)
            {
                bool firstCheck = false;
                bool secondCheck = false;

                for (int i = 0; i < line.Length - 2; i++)
                {
                    // Check chars
                    char firstChar = line[i];
                    char secondChar = line[i + 2];

                    if (firstChar == secondChar)
                    {
                        secondCheck = true;
                    }

                    // Check couples for duplications
                    var firstCouple = $"{line[i]}{line[i + 1]}";
                    for (int j = i + 2; j < line.Length - 1; j++)
                    {
                        var secondCouple = $"{line[j]}{line[j + 1]}";
                        if (firstCouple == secondCouple)
                        {
                            firstCheck = true;
                            break;
                        }
                    }

                    if (firstCheck && secondCheck)
                    {
                        counter++;
                        break;
                    }
                }
            }

            return counter;
        }

        //Part 1
        public int ProcessInputOne(List<string> input)
        {
            int niceStringsCount = 0;

            var bannedStrings = new List<string>()
            {
                "ab", "cd", "pq", "xy"
            };

            var vowels = new List<string>()
            {
                "a", "e", "i", "o", "u"
            };

            foreach (var line in input)
            {
                bool isFound = true;

                Func<char, bool> doesContainVowels = letter => vowels.Contains(letter.ToString());
                int vowelsCount = line.Count(doesContainVowels);
                bool hasDoubledLetters = false;

                if (vowelsCount >= 3)
                {
                    var letters = line.ToArray();

                    for (var i = 0; i < letters.Length - 1; i++)
                    {
                        var letter = letters[i];
                        var nextLetter = letters[i + 1];
                        if (nextLetter == letter)
                        {
                            hasDoubledLetters = true;
                            break;
                        }
                    }

                    //Check if there at least 1 repeating letter
                    if (hasDoubledLetters == true)
                    {
                        foreach (var bannedString in bannedStrings)
                        {
                            if (line.Contains(bannedString))
                            {
                                isFound = false;
                                break;
                            }
                        }

                        if (isFound == true)
                        {
                            niceStringsCount++;
                        }
                    }
                }
            }

            return niceStringsCount;
        }
    }
}
