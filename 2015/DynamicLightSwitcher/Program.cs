using System.Data.Common;
using System.Reflection.PortableExecutable;

namespace DynamicLightSwitcher
{
    public class Program
    {
        static void Main(string[] args)
        {
            var lightSwitcher = new LightSwitcher();
            lightSwitcher.ReadInput();
            lightSwitcher.ChangeStateOfLights();
        }
    }

    public class LightSwitcher
    {
        //If an element is set to true then the light is On
        public Light[,] Lights { get; private set; }

        //private const int AdjacentLightsCount = 8;

        public LightSwitcher()
        {
            Lights = new Light[100, 100];
        }

        public void ReadInput()
        {
            var path = @"D:\Projects\AdventOfCode\2015\DynamicLightSwitcher\Input.txt";
            using var reader = new StreamReader(path);
            string line;
            int row = 0;

            while ((line = reader.ReadLine()) != null)
            {
                int column = 0;

                foreach (char character in line)
                {
                    if (character == '#')
                    {
                        Lights[row, column] = new Light(true);
                    }

                    else
                    {
                        Lights[row, column] = new Light(false);
                    }

                    column++;
                }

                row++;
            }
        }

        public void ChangeStateOfLights()
        {
            var switchedOnLightsCounter = 0;

            for (int steps = 1; steps <= 100; steps++)
            {
                //Get length(0) - gets rows
                for (int row = 0; row < Lights.GetLength(0); row++)
                {
                    //Get length(1) - gets columns
                    for (int column = 0; column < Lights.GetLength(1); column++)
                    {
                        var currentLight = Lights[row, column];

                        int adjacentLightsTurnedOn = VisitAdjacentLights(row, column);
                        //int switchedOffLights = switchedOnAndOffLightsCount.switchedOffLights;

                        //If light is on
                        if (currentLight.CurrentState == true)
                        {
                            if (adjacentLightsTurnedOn == 2 || adjacentLightsTurnedOn == 3)
                                currentLight.ChangeNextState(true);

                            else
                                currentLight.ChangeNextState(false);
                        }

                        //If light is off
                        if (currentLight.CurrentState == false)
                        {
                            if (adjacentLightsTurnedOn == 3)
                                currentLight.ChangeNextState(true);

                            else
                                currentLight.ChangeNextState(false);
                        }

                        //if (steps == 101)
                        //{
                        //    if (currentLight.CurrentState)
                        //        Console.Write($"#");
                        //    else
                        //        Console.Write($".");
                        //}

                        //if (steps == 101 && currentLight.CurrentState)
                        //{
                        //    switchedOnLightsCounter++;
                        //}
                    }
                }

                ChangeCurrentStateForAllLights();
            }

            Console.WriteLine(CountAllSwitchedOnLights());
        }

        private int CountAllSwitchedOnLights()
        {
            var count = 0;

            for (int row = 0; row < Lights.GetLength(0); row++)
            {
                for (int column = 0; column < Lights.GetLength(1); column++)
                {
                    if (Lights[row, column].CurrentState)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void ChangeCurrentStateForAllLights()
        {
            for (int row = 0; row < Lights.GetLength(0); row++)
            {
                for (int col = 0; col < Lights.GetLength(1); col++)
                {
                    var currentLight = Lights[row, col];
                    currentLight.ChangeCurrentState(currentLight.NextState);
                }
            }

        }

        private int VisitAdjacentLights(int initialRow, int initialColumn)
        {
            int row = initialRow;
            int column = initialColumn;

            int switchedOnLights = 0;
            //int switchedOffLights = 0;

            //Iterate through numbers -1, 0, 1 which are used to calculate adjacent lights row indexes
            for (int i = -1; i < 2; i++)
            {
                row = ChangeIndex(i, initialRow, row);

                //Iterate through numbers -1, 0, 1 which are used to calculate adjacent lights column indexes
                for (int j = -1; j < 2; j++)
                {
                    column = ChangeIndex(j, initialColumn, column);

                    bool isNotInitial = initialColumn != column || initialRow != row;
                    bool isValidIndex = ((row >= 0 && column >= 0) && row <= Lights.GetLength(0) - 1 &&
                        column <= Lights.GetLength(1) - 1) ? true : false;

                    if (isNotInitial && isValidIndex && Lights[row, column].CurrentState == true)
                    {
                        switchedOnLights++;
                    }
                }
            }

            //switchedOffLights = AdjacentLightsCount - switchedOnLights;

            return switchedOnLights;
        }

        //rowColumnData holds either a column or a row value
        private int ChangeIndex(int number, int initialRowColumnData, int rowColumnData)
        {
            switch (number)
            {
                case -1:
                    rowColumnData = initialRowColumnData - 1;
                    break;

                case 0:
                    rowColumnData = initialRowColumnData;
                    break;

                case 1:
                    rowColumnData = initialRowColumnData + 1;
                    break;

                default:
                    break;
            }

            return rowColumnData;
        }


    }

    public class Light
    {
        //Shows if the light is off or on
        public bool CurrentState { get; private set; }

        //Shows if the light is off or on
        public bool NextState { get; private set; }

        public Light(bool currentState) => CurrentState = currentState;

        public void ChangeNextState(bool nextState) => NextState = nextState;

        public void ChangeCurrentState(bool currentState) => CurrentState = currentState;
    }
}
