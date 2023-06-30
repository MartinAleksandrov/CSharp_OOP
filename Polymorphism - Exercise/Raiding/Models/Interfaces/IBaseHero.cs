namespace Raiding.Models.Interfaces
{
    public interface IBaseHero
    {
        string Name { get; }

        long TotalHeroPower { get; }

        void CastAbility();
    }
}
