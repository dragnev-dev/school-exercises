using System;
using System.Collections.Generic;
using System.Text;
using ParkingDog.Models.Abstract;

namespace ParkingDog.Models
{
    public class Parking 
    {
        public Parking(string name, int lightCars, int mediumCars, int heavyCars)
        {
            Name = name;
            LightCars = new List<LightCar>(lightCars);
            MediumCars = new List<MediumCar>(mediumCars);
            HeavyCars = new List<HeavyCar>(heavyCars);
        }

        public string Name { get; set; }

        public List<LightCar> LightCars { get; }
        public List<MediumCar> MediumCars { get; }
        public List<HeavyCar> HeavyCars { get; }

        public bool HasEmptySpot(Car car)
        {
            return car switch
            {
                LightCar _ => LightCars.Count < LightCars.Capacity,
                MediumCar _ => MediumCars.Count < MediumCars.Capacity,
                HeavyCar _ => HeavyCars.Count < HeavyCars.Capacity,
                _ => throw new ApplicationException()
            };
        }
        
        public Car AddCar(Car car)
        {
            if (!HasEmptySpot(car))
                throw new ApplicationException();
            
            switch (car)
            {
                case LightCar lightCar:
                    LightCars.Add(lightCar);
                    break;
                case MediumCar mediumCar:
                    MediumCars.Add(mediumCar);
                    break;
                case HeavyCar heavyCar:
                    HeavyCars.Add(heavyCar);
                    break;
            }

            return car;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Паркирани автомобили в паркинг {Name}:");

            foreach (var car in LightCars)
            {
                sb.AppendLine(car.ToString());
            }

            foreach (var car in MediumCars)
            {
                sb.AppendLine(car.ToString());
            }
            
            foreach (var car in HeavyCars)
            {
                sb.AppendLine(car.ToString());
            }
            
            // TODO: Remove the last newline
            
            return sb.ToString();
        }

        public string CurrentLoadInformation()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Паркинг {Name} разполага със следните места:");

            sb.AppendLine($"Леки автомобили {LightCars.Capacity}, заети {LightCars.Count}");
            sb.AppendLine($"Лекотоварни автомобили {MediumCars.Capacity}, заети {MediumCars.Count}");
            sb.AppendLine($"Тежкотоварни автомобили {HeavyCars.Capacity}, заети {HeavyCars.Count}");
            
            // TODO: Remove the last newline
            
            return sb.ToString();
        }
    }
}