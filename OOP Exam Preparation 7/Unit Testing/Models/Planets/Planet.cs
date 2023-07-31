using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private UnitRepository unitRepository;
        private WeaponRepository weaponRepository;

        private string name;
        private double budget;
        private double militaryPower;


        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;

            unitRepository = new UnitRepository();
            weaponRepository = new WeaponRepository();
        }


        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }
                budget = value;
            }
        }

        public double MilitaryPower
        {
            get => militaryPower = Math.Round(CalculatingMilitaryPower(), 3);
        }


        public IReadOnlyCollection<IMilitaryUnit> Army => unitRepository.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weaponRepository.Models;

        public void AddUnit(IMilitaryUnit unit) =>
            unitRepository.AddItem(unit);

        public void AddWeapon(IWeapon weapon) =>
            weaponRepository.AddItem(weapon);

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");

            if (Army.Any())
            {
                sb.AppendLine($"--Forces: {string.Join(", ", Army.Select(obj => obj.GetType().Name))}");
            }
            else
            {
                sb.AppendLine($"--Forces: No units");
            }

            if (Weapons.Any())
            {
                sb.AppendLine($"--Combat equipment: {string.Join(", ", Weapons.Select(obj => obj.GetType().Name))}");
            }
            else
            {
                sb.AppendLine("--Combat equipment: No weapons");
            }

            sb.AppendLine($"--Military Power: {MilitaryPower}");

            return sb.ToString().TrimEnd();
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public void Spend(double amount)
        {
            if (Budget < amount)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }
            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in unitRepository.Models)
            {
                unit.IncreaseEndurance();
            }
        }

        private double CalculatingMilitaryPower()
        {
            double enduranceSum = unitRepository.Models.Sum(s => s.EnduranceLevel);
            double destuctionSum = weaponRepository.Models.Sum(w => w.DestructionLevel);

            double totalAmount = enduranceSum + destuctionSum;

            if (Army.FirstOrDefault(a => a.GetType().Name == nameof(AnonymousImpactUnit)) != null)
            {
                totalAmount *= 1.3;
            }
            if (Weapons.FirstOrDefault(w => w.GetType().Name == nameof(NuclearWeapon)) != null)
            {
                totalAmount *= 1.45;
            }

            return totalAmount;
        }
    }
}