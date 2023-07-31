using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private readonly List<IMilitaryUnit> units;

        public UnitRepository()
        {
            units = new List<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models => units;

        public void AddItem(IMilitaryUnit model) =>
            units.Add(model);

        public IMilitaryUnit FindByName(string name) =>
            units.FirstOrDefault(u => u.GetType().Name == name);

        public bool RemoveItem(string name) =>
            units.Remove(FindByName(name));
    }
}
