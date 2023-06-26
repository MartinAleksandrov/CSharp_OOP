namespace Military_Elite.Models.Interfaces
{
    public interface IMissions 
    {
        string CodeName { get; }
        string State { get; }

        void CompleteMission();
    }
}
