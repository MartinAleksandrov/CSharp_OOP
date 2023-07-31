using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models;
using Heroes.Repositories;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private readonly HeroRepository heroes;
        private readonly WeaponRepository weapons;

        public Controller()
        {
            this.heroes = new HeroRepository();
            this.weapons = new WeaponRepository();
        }


        public string CreateHero(string type, string name, int health, int armour)
        {
            if (this.heroes.Models.FirstOrDefault(h => h.Name == name) != null)
                return string.Format(OutputMessages.HeroAlreadyExist, name);

            if (type != nameof(Barbarian) && type != nameof(Knight))
                return string.Format(OutputMessages.HeroTypeIsInvalid);

            IHero hero = null;
            if (type == nameof(Barbarian))
            {
                hero = new Barbarian(name, health, armour);
            }
            else if (type == nameof(Knight))
            {
                hero = new Knight(name, health, armour);
            }

            this.heroes.Add(hero);
            if (type == nameof(Barbarian))
                return string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);

            return string.Format(OutputMessages.SuccessfullyAddedKnight, name);
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            if (this.weapons.Models.FirstOrDefault(w => w.Name == name) != null)
                return string.Format(OutputMessages.WeaponAlreadyExists, name);

            if (type != nameof(Mace) && type != nameof(Claymore))
                return string.Format(OutputMessages.WeaponTypeIsInvalid);

            IWeapon weapon = null;
            if (type == nameof(Mace))
            {
                weapon = new Mace(name, durability);
            }
            else if (type == nameof(Claymore))
            {
                weapon = new Claymore(name, durability);
            }

            this.weapons.Add(weapon);

            return string.Format(OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name);
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            if (this.heroes.Models.FirstOrDefault(h => h.Name == heroName) == null)
                return string.Format(OutputMessages.HeroDoesNotExist, heroName);

            if (this.weapons.Models.FirstOrDefault(w => w.Name == weaponName) == null)
                return string.Format(OutputMessages.WeaponDoesNotExist, weaponName);

            var hero = this.heroes.Models.FirstOrDefault(h => h.Name == heroName);
            if (hero.Weapon != null)
                return string.Format(OutputMessages.HeroAlreadyHasWeapon, heroName);

            var weapon = this.weapons.Models.FirstOrDefault(w => w.Name == weaponName);

            hero.AddWeapon(weapon);

            return string.Format(OutputMessages.WeaponAddedToHero, heroName, weapon.GetType().Name.ToLower());
        }

        public string StartBattle()
        {
            var map = new Map();
            return map.Fight(this.heroes.Models.ToList());
        }

        public string HeroReport()
        {
            
            List<IHero> myHeroes = heroes.Models.
                OrderBy(n=> n.GetType().Name).
                ThenByDescending(h=>h.Health).
                OrderBy(n =>n.Name).
                ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var hero in myHeroes)
            {
                string heroWeapon = "Unarmed";
                if (hero.Weapon != null)
                {
                    heroWeapon = hero.Weapon.Name;
                }
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: { hero.Health }");
                sb.AppendLine($"--Armour: { hero.Armour }");
                sb.AppendLine($"--Weapon: {heroWeapon}");
            }

            return sb.ToString().TrimEnd();

        }
    }
}

