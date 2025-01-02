using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindShortestDistance
{
    public class Program
    {
        // Driver code
        public static void Main()
        {
            var findDistance = new FindDistance();
            //int[] a = { 1, 2, 3 };
            //findDistance.HeapPermutation(a, a.Length, a.Length);
            var cities = findDistance.ReadInput();

            findDistance.PermutateCities(cities, cities.Count, cities.Count);
            Console.WriteLine($"{findDistance.ShortestDistance}");

        }
    }

    public class FindDistance
    {
        public int ShortestDistance { get; private set; }

        //public List<string> Cities { get; private set; }

        //public Dictionary<List<string>, int> TripsWithDistances { get; private set; }

        public List<CityPair> TripsWithDistances { get; private set; }

        public FindDistance()
        {
            ShortestDistance = int.MinValue;
            TripsWithDistances = new List<CityPair>();
        }

        public List<string> ReadInput()
        {
            var cities = new List<string>();
            string path = @"C:\Users\Ученик\Documents\Alexander V\Programming\Heap\Input.txt";
            using var reader = new StreamReader(path);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                var seperators = new string[] { "to", " ", "=" };
                var splitLine = line.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                var firstCity = splitLine[0];
                var secondCity = splitLine[1];
                //var pair = new List<string>();
                //pair.Add(firstCity);
                //pair.Add(secondCity);

                var distance = Int32.Parse(splitLine[2]);

                if (!cities.Contains(firstCity))
                    cities.Add(firstCity);

                if (!cities.Contains(secondCity))
                    cities.Add(secondCity);

                //var reversedCities = pair.Reverse<string>().ToList();
                var cityPairOne = new CityPair(firstCity, secondCity, distance);
                //if (!TripsWithDistances.Any(cityPairTwo => cityPairTwo.Equals(cityPairOne)))
                //{
                //    TripsWithDistances.Add(cityPairOne);
                //}
                TripsWithDistances.Add(cityPairOne);
            }

            return cities;
        }

        public void CalculateShortestDistance(List<string> cities, int lengthOfCities)
        {
            var totalDistance = 0;

            for (int i = 0; i < lengthOfCities - 1; i++)
            {
                //var pairOfCities = new List<string> { cities[i], cities[i + 1] };
                var firstCity = cities[i];
                var secondCity = cities[i + 1];
                
                var currCityPair = new CityPair(firstCity, secondCity, 0);
                foreach (var cityPair in TripsWithDistances)
                {
                    if (currCityPair.Equals(cityPair))
                    {
                        totalDistance += cityPair.Distance;
                    }
                }                
            }

            if (ShortestDistance < totalDistance)
            {
                ShortestDistance = totalDistance;
            }
        }

        public void CalculateLongestDistance(List<string> cities, int lengthOfCities)
        {
            var totalDistance = 0;

            for (int i = 0; i < lengthOfCities - 1; i++)
            {
                //var pairOfCities = new List<string> { cities[i], cities[i + 1] };
                var firstCity = cities[i];
                var secondCity = cities[i + 1];
                //public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);
                //if (!TripsWithDistances.Any(cityPair => (cityPair.FirstCity == firstCity && cityPair.SecondCity == secondCity) ||
                //(cityPair.SecondCity == firstCity && cityPair.FirstCity == secondCity)))
                //{
                //    var cityPair = new CityPair(firstCity, secondCity, distance);
                //    TripsWithDistances.Add(cityPair);
                //}
                var currCityPair = new CityPair(firstCity, secondCity, 0);
                foreach (var cityPair in TripsWithDistances)
                {
                    if (currCityPair.Equals(cityPair))
                    {
                        totalDistance += cityPair.Distance;
                    }
                }
            }

            if (ShortestDistance > totalDistance)
            {
                ShortestDistance = totalDistance;
            }
        }

        // Generating permutation using Heap Algorithm
        public void PermutateCities(List<string> cities, int currLength, int lengthOfCities)
        {
            // if size becomes 1 then calculates distance the obtained
            // permutation
            if (currLength == 1)
                CalculateShortestDistance(cities, lengthOfCities);

            for (int i = 0; i < currLength; i++)
            {
                PermutateCities(cities, currLength - 1, lengthOfCities);

                // if size is odd, swap 0th i.e (first) and
                // (size-1)th 
                if (currLength % 2 == 1)
                {
                    var temp = cities[0];
                    cities[0] = cities[currLength - 1];
                    cities[currLength - 1] = temp;
                }

                // If size is even, swap ith and
                // (size-1)th 
                else
                {
                    var temp = cities[i];
                    cities[i] = cities[currLength - 1];
                    cities[currLength - 1] = temp;
                }
            }
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }

    public class CityPair
    {
        public string FirstCity { get; init; }

        public string SecondCity { get; init; }

        public int Distance { get; init; }

        public CityPair(string firstCity, string secondCity, int distance)
        {
            FirstCity = firstCity;
            SecondCity = secondCity;
            Distance = distance;
        }

        //Compare two city pairs and if they contain the same elements even if they are reversed returns true
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is CityPair))
            {
                return false;
            }

            var cityPair = (CityPair)obj;
            bool doesContainCityPairs = (cityPair.FirstCity == this.FirstCity) && (cityPair.SecondCity == this.SecondCity);
            bool doesContainCityPairsReversed = (cityPair.FirstCity == this.SecondCity) && (cityPair.SecondCity == this.FirstCity);

            if (doesContainCityPairs || doesContainCityPairsReversed)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            //Performing BIT wise OR Operation on the generated hashcode values
            //If the corresponding bits are different, it gives 1.
            //If the corresponding bits are the same, it gives 0.
            return FirstCity.GetHashCode() ^ SecondCity.GetHashCode() ^ Distance.GetHashCode();
        }
    }
}
