namespace BirthdayCelebrations.Models.Interfaces
{
    public interface ICitizens : IBirthDate
    {
        string Name { get; }
        int Age { get; }
        string CitizneId { get; }
    }
}
