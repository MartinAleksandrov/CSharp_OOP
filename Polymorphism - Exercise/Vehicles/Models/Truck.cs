namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double truckIncreasedFuelConspumption = 1.6;

        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity) :
            base(fuelQuantity, fuelConsumption, truckIncreasedFuelConspumption, tankCapacity)
        {
            fuelConsumption += truckIncreasedFuelConspumption;
        }

        public override void RefuelVehicle(double liters)
        {
            if (liters <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            if (this.FuelQuantity + liters > this.TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {liters} fuel in the tank");
            }
            base.RefuelVehicle(liters * 0.95);
        }
    }
}
