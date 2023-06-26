using Military_Elite.Classes;
using Military_Elite.Core.Interfaces;
using Military_Elite.IO.Interfaces;
using Military_Elite.Models.Interfaces;

namespace Military_Elite.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private static List<Soldier> soldiers = new List<Soldier>();

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                var tokens = command.Split();
                string type = tokens[0];

                switch (type)
                {
                    case nameof(Private):
                        AddPrivate(tokens);
                        break;
                    case nameof(LieutenantGeneral):
                        AddLeutenantGeneral(tokens);
                        break;
                    case nameof(Engineer):
                        AddEngineer(tokens);
                        break;
                    case nameof(Commando):
                        AddCommando(tokens);
                        break;
                    case nameof(Spy):
                        AddSpy(tokens);
                        break;
                }
            }

            soldiers.ForEach(Console.WriteLine);
        }

        private  void AddSpy(string[] tokens)
        {
            Spy spy = new Spy(tokens[1], tokens[2], tokens[3], int.Parse(tokens[4]));

            soldiers.Add(spy);
        }

        private  void AddCommando(string[] tokens)
        {
            try
            {
                Commando commando = new Commando(tokens[1], tokens[2], tokens[3], decimal.Parse(tokens[4]), tokens[5]);

                for (int i = 6; i < tokens.Length; i += 2)
                {
                    try
                    {
                        Mission mission = new Mission(tokens[i], tokens[i + 1]);
                        commando.AddMision(mission);
                    }
                    catch (ArgumentException)
                    {

                    }
                }

                soldiers.Add(commando);
            }
            catch (ArgumentException)
            {

            }
        }

        private  void AddEngineer(string[] tokens)
        {
            try
            {
                Engineer engineer = new Engineer(tokens[1], tokens[2], tokens[3], decimal.Parse(tokens[4]), tokens[5]);

                for (int i = 6; i < tokens.Length; i += 2)
                {
                    Repair repair = new Repair(tokens[i], int.Parse(tokens[i + 1]));
                    engineer.AddRepairs(repair);
                }

                soldiers.Add(engineer);
            }
            catch (ArgumentException)
            {

            }
        }

        private static void AddLeutenantGeneral(string[] tokens)
        {
            LieutenantGeneral general = new LieutenantGeneral(tokens[1], tokens[2], tokens[3], decimal.Parse(tokens[4]));

            for (int i = 5; i < tokens.Length; i++)
            {
                Private privat = (Private)soldiers.Single(s => s.Id == tokens[i]);
                general.AddPrivate(privat);
            }

            soldiers.Add(general);
        }

        private static void AddPrivate(string[] tokens)
        {
            Private privat = new Private(tokens[1], tokens[2], tokens[3], decimal.Parse(tokens[4]));

            soldiers.Add(privat);
        }
    
    }
}
