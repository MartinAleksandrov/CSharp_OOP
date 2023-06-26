using System.Net.Http.Headers;


namespace Telephony.Core
{
    using Interfaces;
    using IO.Interfaces;
    using Telephony.Models;
    using Telephony.Models.Interfaces;
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly IStationaryPhone stationaryPhone;
        private readonly ISmartphone smartphone;

        private Engine()
        {
            this.stationaryPhone = new StationaryPhone();
            this.smartphone = new Smartphone();
        }
        public Engine(IReader reader, IWriter writer)
            : this()
        {
            this.reader = reader;
            this.writer = writer;
        }
        public void Run()
        {

            string[] allNumbers = reader.ReadLine().Split();

            string[] allUrls = reader.ReadLine().Split();

            foreach (var number in allNumbers)
            {
                try
                {
                    if (number.Length == 10)
                    {
                        writer.WriteLine(smartphone.Call(number));
                    }
                    else if (number.Length == 7)
                    {
                        writer.WriteLine(stationaryPhone.Call(number));
                    }
                }
                catch (ArgumentException ex)
                {

                    writer.WriteLine(ex.Message);
                }
            }
            foreach (var url in allUrls)
            {
                try
                {
                    writer.WriteLine(smartphone.Browsing(url));
                }
                catch (ArgumentException ex)
                {

                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
