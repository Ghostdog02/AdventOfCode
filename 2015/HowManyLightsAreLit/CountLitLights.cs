using System.Diagnostics;

namespace HowManyLightsAreLit
{
    public class CountLitLights
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var lights = new CountLitLights();
            var input = lights.ReadInput();
            var litLights = lights.ProcessInputTwo(input);
            Console.WriteLine(litLights);
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine(elapsed.Seconds);
        }

        public List<string> ReadInput()
        {
            var path = @"D:\Projects\AdventOfCode\2015\Exercises\HowManyLightsAreLit\Input.txt";
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
            var totalBrightness = 0;
            var seperators = new string[]
            {
               " " , "through", ",", "turn"
            };

            var coordinates = new Dictionary<(int, int), int>();

            foreach (var line in input)
            {
                //var test = "toggle 0,0 through 999,999";
                var instructions = line.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                var startCoordinates = (x1: int.Parse(instructions[1]), y1: int.Parse(instructions[2]));
                var endCoordinates = (x2: int.Parse(instructions[3]), y2: int.Parse(instructions[4]));

                for (int i = startCoordinates.x1; i <= endCoordinates.x2; i++)
                {
                    for (int j = startCoordinates.y1; j <= endCoordinates.y2; j++)
                    {
                        if (coordinates.ContainsKey((i, j)))
                        {
                            //Value of dictionary represents off or on. 1 is for on and 0 for off
                            var (brightness, total) = FollowInstructions(instructions[0], coordinates[(i, j)], totalBrightness);
                            coordinates[(i, j)] = brightness;
                            totalBrightness = total;

                        }

                        else
                        {
                            var initialValue = 0;
                            var (brightness, total) = FollowInstructions(instructions[0], initialValue, totalBrightness);
                            totalBrightness = total;
                            coordinates.Add((i, j), brightness);
                        }
                    }
                }
            }

            return totalBrightness;
        }

        public (int brightness, int total) FollowInstructions(string instruction, int value, int totalBrightness)
        {
            switch (instruction)
            {
                case "toggle":
                    value += 2;
                    totalBrightness += 2;
                    break;
                case "on":
                    value++;
                    totalBrightness++;
                    break;
                case "off":
                    if (value != 0)
                    {
                        value--;
                        totalBrightness--;
                    }
                    break;
                default:
                    break;
            }

            return (brightness: value, total: totalBrightness);
        }

        public int ProcessInputOne(List<string> input)
        {
            var countLitLights = 0;
            var seperators = new string[]
            {
               " " , "through", ",", "turn"
            };

            var coordinates = new Dictionary<(int, int), int>();

            foreach (var line in input)
            {
                var instructions = line.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                var startCoordinates = (x1: int.Parse(instructions[1]), y1: int.Parse(instructions[2]));
                var endCoordinates = (x2: int.Parse(instructions[3]), y2: int.Parse(instructions[4]));

                for (int i = startCoordinates.x1; i <= endCoordinates.x2; i++)
                {
                    for (int j = startCoordinates.y1; j <= endCoordinates.y2; j++)
                    {
                        if (coordinates.ContainsKey((i, j)))
                        {
                            //Value represents off or on. 1 is for on and 0 for off
                            var value = coordinates[(i, j)];
                            var oldValue = value;
                            value = FollowInstructionsOne(instructions[0], value);

                            //If value hasn't changed don't change countLitLights
                            countLitLights = AlterLitLightsCount(oldValue, value, countLitLights);
                            coordinates[(i, j)] = value;
                        }

                        else
                        {
                            var value = 0;
                            var oldValue = value;
                            value = FollowInstructionsOne(instructions[0], value);
                            countLitLights = AlterLitLightsCount(oldValue, value, countLitLights);
                            coordinates.Add((i, j), value);
                        }
                    }
                }
            }

            return countLitLights;
        }

        public int FollowInstructionsOne(string instruction, int value)
        {
            switch (instruction)
            {
                case "toggle":
                    value = value == 1 ? 0 : 1;
                    break;
                case "on":
                    value = 1;
                    break;
                case "off":
                    value = 0;
                    break;
                default:
                    break;
            }

            return value;
        }

        public int AlterLitLightsCount(int oldValue, int value, int countLitLights)
        {
            if (oldValue != value)
            {
                switch (value)
                {
                    case 0:
                        countLitLights--;
                        break;
                    case 1:
                        countLitLights++;
                        break;
                    default:
                        break;
                }
            }

            return countLitLights;
        }
    }
}