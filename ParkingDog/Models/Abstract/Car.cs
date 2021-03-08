using System;
using ParkingDog.Contracts;

namespace ParkingDog.Models.Abstract
{
    public abstract class Car : IParkable
    {
        public Car()
        {
        }
        protected Car(string make, string model)
        {
            Make = make;
            Model = model;
        }

        public string Make { get; private set; }
        public string Model { get; private set; }
        // Parking
        
        public Car Park(Parking parking)
        {
            parking.AddCar(this);

            return this;
        }
        
        public string SetMakeModel(string make, string model)
        {
            if (make.Length < 2 || model.Length < 2 || make.Length > 50 || model.Length > 50)
                throw new ApplicationException(
                    $"Грешка при създаване на превозно средство.\nДопустимата дължина за марка/модел е между {2} и {50} символа всеки");
            
            Make = make;
            Model = model;
            return ToString();
        }

        public override string ToString()
        {
            return $"Марка {Make}, модел {Model}";
        }
    }
}