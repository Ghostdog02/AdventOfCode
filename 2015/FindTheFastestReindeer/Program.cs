namespace FindTheFastestReindeer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var findDistance = new RaceSimulator();
            findDistance.ProcessInput();
            findDistance.SimulateRacePartTwo();
        }
    }

    public class RaceSimulator
    {
        private readonly List<Reindeer> Reindeers;

        public RaceSimulator()
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
        public void SimulateRacePartTwo()
        {
            for (int seconds = 1; seconds <= 2503; seconds++)
            {
                UpdatePositions();
                AwardPoints();
            }

            Console.WriteLine(Reindeers.Max(a => a.Points));
        }

        private void UpdatePositions()
        {
            foreach (var reindeer in Reindeers)
            {
                reindeer.UpdateState();
            }
        }

        private void AwardPoints()
        {
            var maxDistance = Reindeers.Max(reindeer => reindeer.Distance);

            foreach (var reindeer in Reindeers.Where(reindeer => reindeer.Distance == maxDistance))
            {
                if (reindeer.Distance == maxDistance)
                {
                    reindeer.Points++;
                }
            }
        }
    }

    public class Reindeer
    {
        public readonly int KilometersPerSecond;

        public readonly int MovingTime;

        public readonly int RestTime;

        public int RestTimeLeft { get; private set; }

        public int Distance { get; private set; }

        public int Points { get; set; }

        public bool IsResting { get; private set; }

        public int MovingTimeLeft { get; private set; }

        public Reindeer(int kilometersPerSecond, int movingTime, int restTime)
        {
            KilometersPerSecond = kilometersPerSecond;
            MovingTime = movingTime;
            RestTime = restTime;
            MovingTimeLeft = MovingTime;
            RestTimeLeft = restTime;
            IsResting = false;
        }

        public void UpdateState()
        {
            if (!IsResting)
            {
                Distance += KilometersPerSecond;
                MovingTimeLeft--;

                // Check if we need to transition to resting
                if (MovingTimeLeft == 0)
                {
                    IsResting = true;
                    MovingTimeLeft = MovingTime;
                }
            }

            else
            {
                RestTimeLeft--;

                // Check if we need to transition to moving
                if (RestTimeLeft == 0)
                {
                    IsResting = false;
                    RestTimeLeft = RestTime;
                }

            }
        }
    }
}
