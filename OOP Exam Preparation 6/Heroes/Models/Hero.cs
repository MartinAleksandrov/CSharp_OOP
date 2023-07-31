using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models
{
    public abstract class Hero : IHero
    {
        public Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour = armour;
        }

        private string name;
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HeroNameNull);
                }
                name = value;
            }
        }


        private int health;
        public int Health
        {
            get { return health; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.HeroHealthBelowZero);
                }
                health = value;
            }
        }


        private int armour;
        public int Armour
        {
            get { return armour; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.HeroArmourBelowZero);
                }
                armour = value;
            }
        }


        private bool isAlive;
        public bool IsAlive => this.health > 0;


        private IWeapon weapon;
        public IWeapon Weapon
        {
            get { return weapon; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.WeaponNull);
                }
                weapon = value;
            }
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            int pointsleft = this.Armour - points;

            if (pointsleft>=0)
            {
                this.Armour = pointsleft;
            }
            else
            {
                this.Armour = 0;

                var damage = -pointsleft;

                var healthLeft = this.Health - damage;

                if (healthLeft<0)
                {
                    this.Health = 0;
                }
                else
                {
                    this.Health = healthLeft;
                }
            }
        }
    }
}
