namespace FindTotalAreaOfWrappingPaper
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FindTotalAreaOfWrappingPaper());

        }

        static int FindTotalAreaOfWrappingPaper()
        {
            string path = @"D:\Projects\AdventOfCode\2015\Exercises\FindTotalAreaOfWrappingPaper\Input.txt";
            using var reader  = new StreamReader(path);

            string[] dimensions = reader.ReadToEnd().Split("\r\n");
            //int totalArea = 0;
            int totalLenghtOfRibbon = 0;
            for (int i = 0; i < dimensions.Length; i++)
            {
                //Find dimensions
                string[] dimension = dimensions[i].Split('x');
                int length = int.Parse(dimension[0]);
                int width = int.Parse(dimension[1]);
                int height = int.Parse(dimension[2]);
                //int areaPerPresent = CalculateTotalAreaOfWrappingPaperPerPresent(lenght, width, height);
                //totalArea += areaPerPresent;
                int ribbonPerPresent = FindSmallestPerimiter(length, width, height) + FindVolumeOfPresent(length, width, height);
                totalLenghtOfRibbon += ribbonPerPresent;
            }

            return totalLenghtOfRibbon;
            //return totalArea;
        }

        static int CalculateTotalAreaOfWrappingPaperPerPresent(int lenght, int width, int height)
        {
            int areaOfSideOne = lenght * width;
            int areaOfSideTwo = width * height;
            int areOfSideThree = height * lenght;
            int extraPaperArea = Math.Min(areaOfSideOne, Math.Min(areaOfSideTwo, areOfSideThree));
            int totalArea = 2 * (areaOfSideOne + areaOfSideTwo + areOfSideThree) + extraPaperArea;
            return totalArea;
        }

        static int FindSmallestPerimiter(int lenght, int width, int height)
        {
            int smallestNumber = Math.Min(lenght, Math.Min(width, height));
            int secondSmallestNumber = 0;

            if (smallestNumber == lenght)
            {
                secondSmallestNumber = Math.Min(width, height);
            }

            else if (smallestNumber == width)
            {
                secondSmallestNumber = Math.Min(lenght, height);
            }

            else
            {
                secondSmallestNumber = Math.Min(lenght, width);
            }

            int smallestPerimiter = 2 * (smallestNumber + secondSmallestNumber);
            return smallestPerimiter;
        }

        static int FindVolumeOfPresent(int lenght, int width, int height)
        {
            return lenght * height * width;
        }
    }
}
