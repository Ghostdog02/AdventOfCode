using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Matchsticks
{
    public class FindLength
    {
        public int CodeLength { get; private set; }

        public int InMemoryLength { get; private set; }

        public int EncodedCodeLength { get; private set; }

        readonly public List<string> EscapedSequences = new()
        {
            "\\", "\""
        };

        static void Main(string[] args)
        {
            var findLength = new FindLength();
            var input = findLength.ReadInput();
            Console.WriteLine(findLength.ProcessInputTwo(input));
        }

        public List<string> ReadInput()
        {
            var path = @"D:\Projects\AdventOfCode\2015\Exercises\Matchsticks\Input.txt";
            string line;
            using var reader = new StreamReader(path);
            var lines = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }

            return lines;
        }

        public int ProcessInputOne(List<string> input)
        {
            foreach (var line in input)
            {
                var unescapedLine = Regex.Unescape(line);
                InMemoryLength += unescapedLine.Length - 2;
                CodeLength += line.Length;
            }

            return CodeLength - InMemoryLength;
        }

        public int ProcessInputTwo(List<string> input)
        {
            foreach (var line in input)
            {
                var encodedLine = line;

                foreach (var escapeSequence in EscapedSequences)
                {
                    if (encodedLine.Contains(escapeSequence))
                    {
                        encodedLine = encodedLine.Replace(escapeSequence, escapeSequence + escapeSequence);
                    }
                }

                encodedLine = encodedLine.Insert(0, "\"");
                encodedLine = encodedLine.Insert(encodedLine.Length - 1, "\"");
                EncodedCodeLength += encodedLine.Length;
                CodeLength += line.Length;
            }

            return EncodedCodeLength - CodeLength;
        }
    }
}
