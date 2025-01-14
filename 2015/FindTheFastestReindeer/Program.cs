namespace FindTheFastestReindeer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var findDistance = new FindWinningReindeerDistance();
            findDistance.ProcessInput();
            findDistance.FindFurthestDistance();
        }
    }

    public class FindWinningReindeerDistance
    {
        public List<Reindeer> Reindeers { get; }

        public FindWinningReindeerDistance()
        {
            Reindeers = new List<Reindeer>();
        }

        public void ProcessInput()
        {
            var path = @"D:\Projects\AdventOfCode\2015\FindTheFastestReindeer\Input.txt";
            string line;
            using var reader = new StreamReader(path);
            
            while ((line = reader.ReadLine()) != null)
            {
                var splitLine = line.Split(' ');
                var speed = int.Parse(splitLine[3]);
                var movingTime = int.Parse(splitLine[6]);
                var restTime = int.Parse(splitLine[13]);

                Reindeers.Add(new Reindeer(speed, movingTime, restTime));
            }
        }

        //First Part
        //public void FindFurthestDistance()
        //{
        //    var distance = 0;
        //    var counter = 0;
        //    foreach (var reindeer in Reindeers)
        //    {
        //        var currentSeconds = 0;

        //        while (currentSeconds <= 2503)
        //        {
        //            var totalTime = reindeer.MovingTime + reindeer.RestTime;

        //            if (reindeer.MovingTime + currentSeconds > 2503)
        //            {
        //                var timeLeft = 2503 - currentSeconds;
        //                reindeer.Distance += timeLeft * reindeer.KilometersPerSecond;

        //                break;
        //            }

        //            var currentDistance = reindeer.KilometersPerSecond * reindeer.MovingTime;
        //            reindeer.Distance += currentDistance;
        //            currentSeconds += totalTime;
        //            counter++;
        //        }

        //        if (reindeer.Distance > distance)
        //        {
        //            distance = reindeer.Distance;
        //        }
        //    }

        //    Console.WriteLine(distance);
        //}
    }

    public class Reindeer
    {
        public int KilometersPerSecond { get; }

        public int MovingTime { get; }

        public int RestTime { get; }

        public int Distance { get; set; }

        public Reindeer(int kilometersPerSecond, int movingTime, int restTime)
        {
            KilometersPerSecond = kilometersPerSecond;
            MovingTime = movingTime;
            RestTime = restTime;
        }
    }
}
