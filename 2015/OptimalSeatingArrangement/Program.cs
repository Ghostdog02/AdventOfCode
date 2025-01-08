namespace OptimalSeatingArrangement
{
    public class Program
    {


        static void Main(string[] args)
        {
            string path = @"D:\Projects\AdventOfCode\2015\OptimalSeatingArrangement\Input.txt";
            var arrangement = new FindOptimalArrangement();
            arrangement.ProcessInput(path);
        }


    }

    public class FindOptimalArrangement
    {
        public Dictionary<(string, string), int> PeopleWithHappiness { get; }

        public List<string> Names { get; }

        public FindOptimalArrangement()
        {
            PeopleWithHappiness = new Dictionary<(string, string), int>();
            Names = new List<string>();
        }

        public void ProcessInput(string path)
        {
            string line;
            using var reader = new StreamReader(path);
            //var lines = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                //1st and last element - names
                var words = line.Split(' ');
                var firstName = words[0];
                var lastName = words[words.Length - 1].Trim('.');
                var operation = words[2];
                var points = words[3];

                if (!Names.Contains(firstName))
                    Names.Add(firstName);

                if (!Names.Contains(lastName))
                    Names.Add(lastName);

                switch (operation)
                {
                    case "lose":
                        //Add two people's names as key and reverse the sign of the number as the operation is lose
                        PeopleWithHappiness.Add((firstName, lastName), int.Parse(points.Insert(0, "-")));
                        break;

                    case "gain":
                        PeopleWithHappiness.Add((firstName, lastName), int.Parse(points));
                        break;

                    default:
                        break;
                }
            }
        }

        public void FindOptimalSeatingArrangement()
        {

        }
    }
}
