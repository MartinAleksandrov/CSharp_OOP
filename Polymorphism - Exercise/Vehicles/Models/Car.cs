namespace Vehicles.Models
{
    public class Car : Vehicle
    {
        private const double carIncreasedFuelConspumption = 0.9;

        public Car(double fuelQuantity, double fuelConsumption, double tankCapacity) :
            base(fuelQuantity, fuelConsumption, carIncreasedFuelConspumption, tankCapacity)
        {
            fuelConsumption += carIncreasedFuelConspumption;
        }
    }
}
