namespace Vehicles.Models.Interfaces
{
    public interface IVehicle
    {
        double FuelQuantity { get; }

        double FuelConsumption { get; }

        double TankCapacity { get; }
        void AdditionalFuel(string param);

        void DriveVehicle(double distance);

        void RefuelVehicle(double liters);
    }
}
