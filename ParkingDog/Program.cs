using System;
using System.Linq;

namespace ParkingDog
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var input = ReadInputString();
                ProcessCommand(input);
            }
        }

        private static string ReadInputString()
        {
            return Console.ReadLine();
        }

        private static void ProcessCommand(string inputString)
        {
            var input = inputString.Split();
            var command = input[0];
            var arguments = input.Skip(1).ToArray();
            
            switch (command)
            {
                case "паркинг":
                    var name = input[1];
                    var automobiles = ParseNumericInput(input[2]);
                    var lightCommercialVehicles = ParseNumericInput(input[3]);
                    var heavyGoodsVehicles = ParseNumericInput(input[4]);
                    
                    break;
                case "край":
                    TerminateApplication();
                    break;
                default:
                    InformTheUser("Непозната команда.");
                    break;
            }
        }

        private static int ParseNumericInput(string input)
        {
            // try.parse
            return int.Parse(input);
        }

        private static void TerminateApplication()
        {
            Environment.Exit(0);
        }

        private static void InformTheUser(string message)
        {
            Console.WriteLine(message);
        }
    }
}
