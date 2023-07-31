using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models
{
    public class Mace : Weapon
    {
        public Mace(string name, int durability) : base(name, durability)
        {
        }

        public override int DoDamage()
        {
            int damage = 25;
          
            if (this.Durability - 1 < 0)
            {
                return 0;
            }
            this.Durability -= 1;

            return damage;
        }
    }
}
