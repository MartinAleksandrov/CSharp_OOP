using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters
{
    public class RaceMotorcycle : Motorcycle
    {
        private const double DefaultFuelConsumption = 8;
        public RaceMotorcycle(int horsepower, double fuel) : base(horsepower, fuel)
        {

        }
        public override double FuelConsumption => DefaultFuelConsumption;


    }
}
