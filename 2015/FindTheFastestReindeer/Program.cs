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
            var currentFurthestDistance = 0;
            var counter = 0;

            for (int seconds = 1; seconds <= 1000; seconds++)
            {
                foreach (var reindeer in Reindeers)
                {
                    if (seconds == 140)
                    {
                        int b = 0;
                    }

                    if (seconds % reindeer.StartMovingOn == 0)
                    {
                        reindeer.IsResting = false;
                        counter++;
                    }
                    //else if (seconds % (reindeer.MovingTime + 1) == 0)
                    //    reindeer.IsResting = true;

                    if (reindeer.IsResting == false)
                    {
                        if (reindeer.MovingTimeLeft == 0)
                        {
                            reindeer.IsResting = true;
                            reindeer.MovingTimeLeft = reindeer.MovingTime;
                        }


                        else
                            reindeer.MovingTimeLeft--;
                    }

                    //if (i == reindeer.MovingTime + 1)
                    //    reindeer.IsResting = true;

                    if (!reindeer.IsResting)
                        reindeer.Distance += reindeer.KilometersPerSecond;

                    if (reindeer.Distance > currentFurthestDistance)
                        currentFurthestDistance = reindeer.Distance;
                }

                foreach (var reindeer in Reindeers)
                {
                    if (reindeer.Distance == currentFurthestDistance)
                        reindeer.Points++;
                }
            }

            Console.WriteLine(Reindeers.Max(a => a.Points));
            Console.WriteLine(counter);
        }
    }

    public class Reindeer
    {
        public readonly int KilometersPerSecond;

        public readonly int MovingTime;

        public readonly int RestTime;

        //Every amount of seconds that reindeer should move on
        public readonly int StartMovingOn;

        public int Distance { get; set; }

        public int Points { get; set; }

        public bool IsResting { get; set; }

        public int MovingTimeLeft { get; set; }

        public Reindeer(int kilometersPerSecond, int movingTime, int restTime)
        {
            KilometersPerSecond = kilometersPerSecond;
            MovingTime = movingTime;
            RestTime = restTime;
            StartMovingOn = MovingTime + RestTime + 1;
            MovingTimeLeft = MovingTime;
        }
    }
}
