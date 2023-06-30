using Raiding.Factories.Interfaces;
using Raiding.Models.Contracts;
using Raiding.Models.Interfaces;

namespace Raiding.Factories
{
    public class HeroFactory : IHeroFactory
    {
        public IBaseHero CreateHero(string heroName, string heroType)
        {
            IBaseHero hero = null;

            try
            {
                if (heroType == "Druid")
                {
                    hero = new Druid(heroName);
                }
                else if (heroType == "Paladin")
                {
                    hero = new Paladin(heroName);
                }
                else if (heroType == "Rogue")
                {
                    hero = new Rogue(heroName);
                }
                else if (heroType == "Warrior")
                {
                    hero = new Warrior(heroName);
                }
                else
                {
                    throw new ArgumentException($"Invalid hero!");
                }
            }
            catch (ArgumentException ex )
            {
                Console.WriteLine(ex.Message);
            }
            return hero;
        }
    }
}
