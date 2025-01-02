namespace FindFloor
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = "D:\\Projects\\AdventOfCode\\2015\\Exercises\\FindFloor\\Instructions.txt";
            Console.WriteLine(ReadInstructions(filePath));
        }

        static int ReadInstructions(string filePath)
        {
            using StreamReader sr = new StreamReader(filePath);
            string line = sr.ReadLine();
            int floor = 0;
            int basementPosition = 0;
            int position = 0;
            while (line != null)
            {
                foreach (var bracket in line)
                {
                    position++;
                    if (bracket == '(')
                    {
                        floor++;
                    }

                    else
                    {
                        floor--;
                        if (floor == -1)
                        {
                            basementPosition = position;
                            return basementPosition;
                        }
                        
                    }
                }
                
                line = sr.ReadLine();
            }

            return floor;
        }


    }
}
