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
            ParkingLots = new List<Parking>();
            while (true)
            {
                var input = ReadInputString();
                ProcessCommand(input);
            }
        }

        private static List<Parking> ParkingLots { get; set; }

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
                        _ => throw new AggregateException()
                    };
                    car.SetMakeModel(input[1], input[2]);

                    var parkingCandidate = GetAvailableParkingSpot(car);
                    if (parkingCandidate != null)
                        car.Park(parkingCandidate);

                    else
                        InformTheUser($"Няма свободни паркоместа за {car.Make} {car.Model}!");

                    break;
                case "печат":
                    var printParking = ParkingLots.FirstOrDefault(p => p.Name == input[1]);

                    InformTheUser(printParking != null
                        ? printParking.ToString()
                        : $"Паркинг с име {input[1]} не е намерен.");
                    
                    break;
                case "паркинг":
                    var name = input[1];
                    var lightCars = ParseUserEnteredNumbers(input[2], typeof(int));
                    var mediumCars = ParseUserEnteredNumbers(input[3], typeof(int));
                    var heavyCars = ParseUserEnteredNumbers(input[4], typeof(int));

                    var parking = new Parking(name, lightCars, mediumCars, heavyCars);

                    ParkingLots.Add(parking);
                    break;
                case "край":
                    foreach (var item in ParkingLots)
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

        private static Parking GetAvailableParkingSpot(Car car)
        {
            return ParkingLots.FirstOrDefault(parking => parking.HasEmptySpot(car));
        }

        private static int ParseUserEnteredNumbers(string input, Type type)
        {
            try
            {
                if (type == typeof(int))
                    return int.Parse(input);
                throw new AggregateException();
            }
            catch
            {
                throw new ApplicationException($"Невалидна стойност: {input}");
            }
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