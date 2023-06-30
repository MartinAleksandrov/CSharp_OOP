using Raiding.Models.Contracts;
using Raiding.Models.Interfaces;

namespace Raiding.Factories.Interfaces
{
    public interface IHeroFactory
    {
        IBaseHero CreateHero(string heroName, string heroType);
    }
}
