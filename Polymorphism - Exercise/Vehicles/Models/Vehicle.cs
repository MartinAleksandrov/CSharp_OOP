using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public abstract class Vehicle : IVehicle
    {
        private double IncreasedFuelConspumption;
        private double fuelQuantity;
        private double check;

        protected Vehicle(double fuelQuantity, double fuelConsumption, double increasedFuelConspumption, double tankCapacity)
        {
            TankCapacity = tankCapacity;
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption + increasedFuelConspumption;
        }

        public double FuelQuantity
        {
            get => fuelQuantity;
            private set
            {
                if (value > TankCapacity)
                {
                    fuelQuantity = 0;
                }
                else
                {
                    fuelQuantity = value;
                }
            }
        }

        public double FuelConsumption { get; private set; }

        public double TankCapacity { get; private set; }

        public void AdditionalFuel(string param)
        {
            if (check==0 && param=="Drive")
            {
                this.FuelConsumption += 1.4;
                check++;
            }
            else if (check > 0 &&param == "DriveEmpty")
            {
                this.FuelConsumption -= 1.4;
                check=0;
            }
        }
        public virtual void DriveVehicle(double distance)
        {
            if (this.FuelQuantity < this.FuelConsumption * distance)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }

            FuelQuantity -= this.FuelConsumption * distance;
            Console.WriteLine($"{this.GetType().Name} travelled {distance} km");
        }

        public virtual void RefuelVehicle(double liters)
        {
            if (liters <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            if (FuelQuantity + liters > TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {liters} fuel in the tank");
            }
            FuelQuantity += liters;
        }
        public override string ToString()
        {
            return $"{this.GetType().Name}: {FuelQuantity:F2}";
        }
    }
}
