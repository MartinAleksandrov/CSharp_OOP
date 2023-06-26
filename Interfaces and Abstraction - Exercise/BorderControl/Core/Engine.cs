namespace BorderControl.Core
{
    using BorderControl.Core.Interfaces;
    using BorderControl.IO.Interfaces;
    using BorderControl.Models;
    using BorderControl.Models.Interfaces;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly ICitizens citizens;
        private readonly IRobot robot;

        public Engine(string name,int age,string id)
        {
            this.citizens = new Citizens(name,age,id);
        }
        public Engine(string model,string id)
        {
            this.robot = new Robot(model,id);
        }
        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            List<string> Ids = new();

            string input = string.Empty;
            while ((input = reader.ReadLine()) !="End")
            {
                string[] commArgs = input.Split();

                if (commArgs.Length==3)
                {
                    string citizenName = commArgs[0];
                    int age = int.Parse(commArgs[1]);
                    string citizenId = commArgs[2];

                    ICitizens citizen = new Citizens(citizenName,age,citizenId);
                    Ids.Add(citizenId);
                }
                else
                {
                    string robotName = commArgs[0];
                    string robotId = commArgs[1];

                    IRobot robot = new Robot(robotName,robotId);
                    Ids.Add(robotId);
                }
            }
            string fakeIds = reader.ReadLine();
            foreach (var id in Ids)
            {
                var result = id.Substring(id.Length - fakeIds.Length);
                if (fakeIds==result)
                {
                    writer.WriteLine(id);
                }
            }
        }
    }
}
