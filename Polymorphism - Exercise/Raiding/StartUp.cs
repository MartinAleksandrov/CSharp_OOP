using Raiding.Factories;
using Raiding.Models.Contracts;
using Raiding.Models.Interfaces;
using System.Diagnostics.Metrics;

namespace Raiding;

public class StartUp
{
    public static void Main()
    {
        List<IBaseHero> list = new List<IBaseHero>();

        int n = int.Parse(Console.ReadLine());

        HeroFactory factory = new HeroFactory();
        int validHeroes = 0;

        while (n != validHeroes)
        {
            string name = Console.ReadLine();
            string heroType = Console.ReadLine();

            try
            {
                var hero = factory.CreateHero(name, heroType);
                if (hero!=null)
                {
                    list.Add(hero);
                    validHeroes++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }
        }
        foreach (var hero in list)
        {
           hero.CastAbility();
        }

        long bossPower = long.Parse(Console.ReadLine());

        long totalheroPower = 0;

        foreach (var item in list)
        {
            totalheroPower += item.TotalHeroPower;
        }

        if (totalheroPower>=bossPower)
        {
            Console.WriteLine("Victory!");
        }
        else
        {
            Console.WriteLine("Defeat...");
        }
    }
}
