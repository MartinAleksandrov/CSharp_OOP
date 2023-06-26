namespace Telephony.Models.Interfaces
{
    public interface ISmartphone : IStationaryPhone
    {
        string Browsing(string url);
    }
}
