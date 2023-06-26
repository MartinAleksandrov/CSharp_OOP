namespace Military_Elite.Classes
{
    using Models.Interfaces;
    using System.Collections.Generic;
    using System.Text;

    public class Commando : SpecialisedSoldier, IComando
    {
        private List<IMissions> missions;
        public Commando(string id, string firstName, string lastName, decimal salary, string corps) :
            base(id, firstName, lastName, salary, corps)
        {
            missions = new List<IMissions>();
        }

        public IReadOnlyCollection<IMissions> Missions { get; }

        public void AddMision(Mission mission)
        {
            missions.Add(mission);
        }
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(base.ToString())
                .AppendLine("Missions:");

            this.missions
                .ForEach(m => builder.AppendLine(m.ToString()));

            return builder.ToString().TrimEnd();
        }
    }
}

