using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private readonly PlanetRepository planets;

        public Controller()
        {
            planets = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            else if (unitTypeName != nameof(AnonymousImpactUnit) &&
                unitTypeName != nameof(SpaceForces) && unitTypeName != nameof(StormTroopers))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }
            else if (planet.Army.FirstOrDefault(p => p.GetType().Name == unitTypeName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            IMilitaryUnit unit;
            if (unitTypeName == nameof(AnonymousImpactUnit))
            {
                unit = new AnonymousImpactUnit();
            }
            else if (unitTypeName == nameof(SpaceForces))
            {
                unit = new SpaceForces();
            }
            else
            {
                unit = new StormTroopers();
            }

            planet.Spend(unit.Cost);
            planet.AddUnit(unit);
            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            else if (planet.Weapons.FirstOrDefault(p => p.GetType().Name == weaponTypeName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, weaponTypeName, planetName));
            }
            else if (weaponTypeName != nameof(BioChemicalWeapon) &&
               weaponTypeName != nameof(NuclearWeapon) && weaponTypeName != nameof(SpaceMissiles))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            IWeapon weapon;
            if (weaponTypeName == nameof(BioChemicalWeapon))
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == nameof(NuclearWeapon))
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);
            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            IPlanet planet = planets.FindByName(name);

            if (planet != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }

            planet = new Planet(name, budget);

            planets.AddItem(planet);
            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p=>p.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPLanet = planets.FindByName(planetOne);
            IPlanet secondPlanet = planets.FindByName(planetTwo);

            if (firstPLanet.MilitaryPower == secondPlanet.MilitaryPower)
            {
                IWeapon weapon1 = firstPLanet.Weapons.FirstOrDefault(w => w.GetType().Name == nameof(NuclearWeapon));
                IWeapon weapon2 = secondPlanet.Weapons.FirstOrDefault(w => w.GetType().Name == nameof(NuclearWeapon));

                if (weapon1 != null && weapon2 == null)
                {
                    return WiningPlanet1(planetOne, planetTwo, firstPLanet, secondPlanet);
                }
                else if (weapon1 == null && weapon2 != null)
                {
                    return WiningPlanet2(planetOne, planetTwo, firstPLanet, secondPlanet);

                }
                else
                {
                    firstPLanet.Spend(firstPLanet.Budget * 0.5);
                    secondPlanet.Spend(secondPlanet.Budget * 0.5);

                    return OutputMessages.NoWinner;
                }
            }
            else
            {
                if (firstPLanet.MilitaryPower > secondPlanet.MilitaryPower)
                {
                    return WiningPlanet1(planetOne, planetTwo, firstPLanet, secondPlanet);
                }
                else
                {
                    return WiningPlanet2(planetOne, planetTwo, firstPLanet, secondPlanet);
                }
            }

        }
        private string WiningPlanet1(string planetOne, string planetTwo, IPlanet firstPLanet, IPlanet secondPlanet)
        {
            firstPLanet.Spend(firstPLanet.Budget * 0.5);
            firstPLanet.Profit(secondPlanet.Budget * 0.5);

            double sumOfAllWeaponsUnits = secondPlanet.Army.Sum(u => u.Cost) + secondPlanet.Weapons.Sum(w => w.Price);

            firstPLanet.Profit(sumOfAllWeaponsUnits);
            planets.RemoveItem(planetTwo);

            return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
        }

        private string WiningPlanet2(string planetOne, string planetTwo, IPlanet firstPLanet, IPlanet secondPlanet)
        {
            secondPlanet.Spend(secondPlanet.Budget * 0.5);
            secondPlanet.Profit(firstPLanet.Budget * 0.5);

            double sumOfAllWeaponsUnits = firstPLanet.Army.Sum(u => u.Cost) + firstPLanet.Weapons.Sum(w => w.Price);

            secondPlanet.Profit(sumOfAllWeaponsUnits);
            planets.RemoveItem(planetOne);
            return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
        }


        public string SpecializeForces(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            else if (!planet.Army.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);
            }
            planet.Spend(1.25);
            planet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }
    }
}
