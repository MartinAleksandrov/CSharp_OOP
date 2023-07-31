using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.MilitaryUnits
{
    public class StormTroopers : MilitaryUnit
    {
        private const double stormTrooperscost = 2.5;
        public StormTroopers() : base(stormTrooperscost)
        {
        }
    }
}
