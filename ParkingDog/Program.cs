using System;
using System.Collections.Generic;
using System.Linq;
using ParkingDog.Models;
using ParkingDog.Models.Abstract;

namespace ParkingDog
{
    class Program
    {
        static void Main()
        {
            Parkings = new List<Parking>();
            while (true)
            {
                var input = ReadInputString();
                ProcessCommand(input);
            }
        }

        private static List<Parking> Parkings { get; set; }

        private static string ReadInputString()
        {
            return Console.ReadLine();
        }

        private static void ProcessCommand(string inputString)
        {
            var input = inputString.Split();
            var command = input[0].ToLower();
            // var arguments = input.Skip(1).ToArray();

            switch (command)
            {
                case "кола": // Automobile
                case "бус": // LightCommercialVehicles
                case "камион": // HeavyGoodsVehicles
                    Car car = command switch
                    {
                        "кола" => new LightCar(),
                        "бус" => new MediumCar(),
                        "камион" => new HeavyCar(),
                        _ => throw new ApplicationException()
                    };
                    car.SetMakeModel(input[1], input[2]);

                    var parkingCandidate = GetParkingSpot(car);
                    if (parkingCandidate != null)
                        car.Park(parkingCandidate);

                    else
                        InformTheUser($"Няма свободни паркоместа за {car.Make} {car.Model}!");

                    break;
                case "печат":
                    var printParking = Parkings.FirstOrDefault(p => p.Name == input[1]);

                    InformTheUser(printParking != null
                        ? printParking.ToString()
                        : $"Паркинг с име {input[1]} не е намерен.");
                    
                    break;
                case "паркинг":
                    var name = input[1];
                    var lightCars = ParseNumericInput(input[2]);
                    var mediumCars = ParseNumericInput(input[3]);
                    var heavyCars = ParseNumericInput(input[4]);

                    var parking = new Parking(name, lightCars, mediumCars, heavyCars);

                    Parkings.Add(parking);
                    break;
                case "край":
                    foreach (var item in Parkings)
                    {
                        InformTheUser(item.CurrentLoadInformation());
                    }

                    TerminateApplication();
                    break;
                default:
                    InformTheUser("Непозната команда.");
                    break;
            }
        }

        private static Parking GetParkingSpot(Car car)
        {
            foreach (var parking in Parkings)
            {
                if (parking.HasEmptySpot(car))
                {
                    return parking;
                }
            }

            return null;
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