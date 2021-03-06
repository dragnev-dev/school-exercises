using ParkingDog.Models.Abstract;

namespace ParkingDog.Models
{
    public class HeavyCar : Car
    {
        public HeavyCar()
        {
        }
        public HeavyCar(string make, string model) : base(make, model)
        {
        }
    }
}