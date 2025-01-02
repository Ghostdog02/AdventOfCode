
namespace ConnectWiresThroughAGate
{
    public class ConnectWires
    {
        public List<string> Gates { get; private set; }

        public Dictionary<string, ushort> Wires { get; private set; }

        public List<string> Input { get; private set; }

        public ConnectWires()
        {
            Gates = new List<string>() { "NOT", "LSHIFT", "RSHIFT", "AND", "OR" };
            Wires = new Dictionary<string, ushort>();
            Input = new List<string>();
        }

        //Part 2
        static void Main(string[] args)
        {
            var connectWires = new ConnectWires();
            connectWires.Input = connectWires.ReadInput();
            connectWires.ProcessInput();
            connectWires.EmptyDictionary(connectWires.Wires, connectWires.Wires["a"]);
            connectWires.ProcessInput();
            Console.WriteLine(connectWires.Wires["a"]);
        }

        public List<string> ReadInput()
        {
            var path = @"D:\Projects\AdventOfCode\2015\Exercises\ConnectWiresThroughAGate\Input.txt";
            string line;
            using var reader = new StreamReader(path);
            var lines = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line.Trim());
            }

            return lines;
        }

        public void ProcessInput()
        {
            var wiresForSearching = new Stack<string>();

            //If it is in the beginning or it has any elements
            while (!Wires.ContainsKey("a") || Wires.Count == 0)
            {
                for (int i = 0; i < Input.Count; i++)
                {
                    var line = Input[i];
                    var instructions = line.Split(new string[] { " ", "->" }, StringSplitOptions.RemoveEmptyEntries);

                    string operation = string.Empty;
                    var isGateFound = false;
                    foreach (var gate in Gates)
                    {
                        if (line.Contains(gate))
                        {
                            operation = gate;
                            isGateFound = true;
                            break;
                        }
                    }

                    //If wire is in the beginning or is wiresForSearching Empty or is found
                    bool isFoundOrIsEmpty = wiresForSearching.Count > 0 ?
                        wiresForSearching.First().Equals(instructions.Last()) :
                        true;

                    if (isFoundOrIsEmpty)
                    {
                        if (isGateFound)
                        {
                            //Gate is in the beginning it means that the gate is NOT
                            if (Gates.Contains(instructions[0]))
                            {
                                //Output Wire
                                var findSignal = FindSignal(instructions[1], wiresForSearching);
                                ushort signal = findSignal.signal;

                                if (findSignal.isSignalInitialized == true)
                                {
                                    //If instructions[2] remove it from the stack because it's signal is found
                                    PopStack(wiresForSearching, instructions[2]);
                                    Wires[instructions[2]] = (ushort)~signal;
                                }

                            }

                            ////Gate is in the middle: AND, OR, LSHIFT, RSHIFT and means we have 4 instructions
                            else
                            {
                                var findFirstSignal = FindSignal(instructions[0], wiresForSearching);
                                ushort firstSignal = findFirstSignal.signal;

                                if (findFirstSignal.isSignalInitialized == true)
                                {
                                    var findSecondSignal = FindSignal(instructions[2], wiresForSearching);
                                    ushort secondSignal = findSecondSignal.signal;

                                    if (findSecondSignal.isSignalInitialized == true)
                                    {
                                        var result = PerformBitwiseOperation(operation, firstSignal, secondSignal);
                                        //If instructions[3] remove it from the stack because it's signal is found
                                        PopStack(wiresForSearching, instructions[3]);
                                        Wires[instructions[3]] = result;
                                    }
                                }

                            }
                        }

                        //If Gate is not found then we will have only one signal
                        else
                        {
                            var findSignal = FindSignal(instructions[0], wiresForSearching);
                            ushort signal = findSignal.signal;

                            if (findSignal.isSignalInitialized == true)
                            {
                                //If instructions[1] remove it from the stack because it's signal is found
                                PopStack(wiresForSearching, instructions[1]);
                                Wires[instructions[1]] = signal;
                            }

                        }
                    }




                }
            }



        }

        public ushort PerformBitwiseOperation(string gate, ushort inputSignal1, ushort inputSignal2 = 0)
        {
            ushort result = 0;

            switch (gate)
            {
                case "AND":
                    result = (ushort)(inputSignal1 & inputSignal2);
                    break;
                case "OR":
                    result = (ushort)(inputSignal1 | inputSignal2);
                    break;
                case "LSHIFT":
                    result = (ushort)(inputSignal1 << inputSignal2);
                    break;
                case "RSHIFT":
                    result = (ushort)(inputSignal1 >> inputSignal2);
                    break;
                default:
                    break;
            }

            return result;
        }

        public (ushort signal, bool isSignalInitialized) FindSignal(string wireSignal, Stack<string> wiresForSearching)
        {
            bool isSignalInitialized = true;
            ushort signal;

            if (ushort.TryParse(wireSignal, out signal) == false)
            {
                //If wires doesn't contain current wire then push it to wiresForSearching
                if (Wires.TryGetValue(wireSignal, out ushort value) == false)
                {
                    if (!wiresForSearching.Contains(wireSignal))
                        wiresForSearching.Push(wireSignal);

                    //Wire isn't in Wires so its signal is not initialized
                    isSignalInitialized = false;

                }

                else
                {
                    bool isEmptyOrIsFound = wiresForSearching.Count > 0 ?
                        wiresForSearching.First().Equals(wireSignal) :
                        false;

                    if (isEmptyOrIsFound)
                        wiresForSearching.Pop();

                    signal = value;
                }


            }

            return (signal, isSignalInitialized);
        }

        public void PopStack(Stack<string> wiresForSearching, string wire)
        {
            bool isEmptyAndIsFound = wiresForSearching.Count > 0 ?
    wiresForSearching.First().Equals(wire) : false;

            if (isEmptyAndIsFound)
            {
                wiresForSearching.Pop();
            }
        }

        public void EmptyDictionary(Dictionary<string, ushort> wires, ushort signalOfB)
        {
            wires.Clear();
            wires.Add("b", signalOfB);
        }
    }
}
