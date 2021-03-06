using ParkingDog.Models.Abstract;

namespace ParkingDog.Models
{
    public class LightCar : Car
    {
        public LightCar()
        {
        }
        public LightCar(string make, string model) : base(make, model)
        {
        }
    }
}