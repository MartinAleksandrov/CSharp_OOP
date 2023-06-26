namespace Telephony.Models
{
    using Interfaces;
    public class Smartphone : IStationaryPhone, ISmartphone
    {
        public string Browsing(string url)
        {
            if (BrowsChecker(url))
            {
                throw new ArgumentException("Invalid URL!");
            }
            return $"Browsing: {url}!";
        }

        public string Call(string number)
        {
            if (!NumChecker(number))
            {
                throw new ArgumentException("Invalid number!");
            }
            return $"Calling... {number}";
        }
        private bool NumChecker(string number) => number.All(ch => char.IsDigit(ch));
        private bool BrowsChecker(string url) => url.Any(ch => char.IsDigit(ch));
    }
}
