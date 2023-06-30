namespace Vehicles.Models
{
    public class Bus : Vehicle
    {
        private const double increasedFuelConspumption= 0;
        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity) :
            base(fuelQuantity, fuelConsumption, increasedFuelConspumption, tankCapacity)
        {
        }

        public override void RefuelVehicle(double liters)
        {
            base.RefuelVehicle(liters);
        }
    }
}
