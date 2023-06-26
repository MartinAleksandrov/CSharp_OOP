namespace Military_Elite.Models.Interfaces
{
    public interface IComando :ISpecialisedSoldier
    {
        IReadOnlyCollection<IMissions> Missions { get; }
    }
}
