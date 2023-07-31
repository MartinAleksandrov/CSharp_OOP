using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Heroes.Models
{
    public abstract class Weapon : IWeapon
    {
        public Weapon(string name, int durability)
        {
            Name = name;
            Durability = durability;
        }

        private string name;
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.WeaponTypeNull);
                }
                name = value;
            }
        }


        private int durability;
        public  int Durability
        {
            get { return durability; }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.DurabilityBelowZero);
                }
                durability = value;
            }
        }
        public abstract int DoDamage();
    }
}
