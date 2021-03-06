using ParkingDog.Models;
using ParkingDog.Models.Abstract;

namespace ParkingDog.Contracts
{
    public interface IParkable
    {
        Car Park(Parking parking);
    }
}