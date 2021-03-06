using ParkingDog.Models.Abstract;

namespace ParkingDog.Models
{
    public class MediumCar : Car
    {
        public MediumCar()
        {
        }
        public MediumCar(string make, string model) : base(make, model)
        {
        }
    }
}