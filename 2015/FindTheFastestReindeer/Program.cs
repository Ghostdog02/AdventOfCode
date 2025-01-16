namespace FindTheFastestReindeer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var findDistance = new FindWinningReindeerDistance();
            findDistance.ProcessInput();
            findDistance.FindFurthestDistancePartTwo();
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
            string? line;
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
        //public void FindFurthestDistancePartOne()
        //{
        //    var distance = 0;
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
        //        }

        //        if (reindeer.Distance > distance)
        //        {
        //            distance = reindeer.Distance;
        //        }
        //    }

        //    Console.WriteLine(distance);
        //}

        //Second Part
        public void FindFurthestDistancePartTwo()
        {
            for (int seconds = 1; seconds <= 2503; seconds++)
            {
                var currentFurthestDistance = 0;

                foreach (var reindeer in Reindeers)
                {
                    if (!reindeer.IsResting)
                    {
                        reindeer.Distance += reindeer.KilometersPerSecond;
                        reindeer.MovingTimeLeft--;

                        // Check if we need to transition to resting
                        if (reindeer.MovingTimeLeft == 0)
                        {
                            reindeer.IsResting = true;
                            reindeer.MovingTimeLeft = reindeer.MovingTime;
                        }
                    }

                    else
                    {
                        reindeer.RestTimeLeft--;

                        // Check if we need to transition to moving
                        if (reindeer.RestTimeLeft == 0)
                        {
                            reindeer.IsResting = false;
                            reindeer.RestTimeLeft = reindeer.RestTime;
                        }

                    }

                    if (reindeer.Distance > currentFurthestDistance)
                        currentFurthestDistance = reindeer.Distance;
                }

                foreach (var reindeer in Reindeers)
                {
                    if (reindeer.Distance == currentFurthestDistance)
                    {
                        reindeer.Points++;

                    }
                }
            }

            Console.WriteLine(Reindeers.Max(a=>a.Points));

        }
    }

    public class Reindeer
    {
        public readonly int KilometersPerSecond;

        public readonly int MovingTime;

        public readonly int RestTime;

        public int RestTimeLeft { get; set; }

        public int Distance { get; set; }

        public int Points { get; set; }

        public bool IsResting { get; set; }

        public int MovingTimeLeft { get; set; }

        public Reindeer(int kilometersPerSecond, int movingTime, int restTime)
        {
            KilometersPerSecond = kilometersPerSecond;
            MovingTime = movingTime;
            RestTime = restTime;
            MovingTimeLeft = MovingTime;
            RestTimeLeft = restTime;
        }
    }
}
