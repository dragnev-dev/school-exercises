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