namespace Telephony.Models
{
    using Interfaces;
    public class StationaryPhone : IStationaryPhone
    {
        public string Call(string number)
        {
            if (!NumChecker(number))
            {
                throw new ArgumentException("Invalid number!");
            }
            return $"Dialing... {number}";

        }
        private bool NumChecker(string number) => number.All(ch => char.IsDigit(ch));
    }
}
