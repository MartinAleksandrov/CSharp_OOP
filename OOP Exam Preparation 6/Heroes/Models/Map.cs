using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Heroes.Models
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            List<IHero> knights = new List<IHero>();
            List<IHero> barbarians = new List<IHero>();

            int knightDeadCount = 0;
            int barbDeadCount = 0;


            foreach (var hero in players)
            {
                if (hero.GetType().Name == nameof(Knight))
                {
                    knights.Add(hero);
                }
                else if (hero.GetType().Name == nameof(Barbarian))
                {
                    barbarians.Add(hero);
                }
            }

            while (knights.Any(k => k.IsAlive) && barbarians.Any(b => b.IsAlive))
            {
                foreach (var knight in knights)
                {
                    if (knight.IsAlive && knight.Weapon != null)
                    {
                        foreach (var barb in barbarians)
                        {
                            if (barb.IsAlive)
                            {
                                barb.TakeDamage(knight.Weapon.DoDamage());
                            }
                        }
                    }
                
                }
                foreach (var barb in barbarians)
                {
                    if (barb.IsAlive && barb.Weapon != null)
                    {
                        foreach (var knight in knights)
                        {
                            if (knight.IsAlive)
                            {
                                knight.TakeDamage(barb.Weapon.DoDamage());
                            }
                        }
                    }
                }
            }
            knightDeadCount = knights.Where(k => !k.IsAlive).Count();
            barbDeadCount = barbarians.Where(b => !b.IsAlive).Count();


            if (!barbarians.Any(b => b.IsAlive))
            {
                return string.Format(OutputMessages.MapFightKnightsWin, knightDeadCount);
            }
            return string.Format(OutputMessages.MapFigthBarbariansWin,barbDeadCount);

        }
    }
}
