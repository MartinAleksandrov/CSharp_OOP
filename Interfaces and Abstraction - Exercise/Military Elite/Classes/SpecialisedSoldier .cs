namespace Military_Elite.Classes
{
    using Military_Elite.Enums;
    using Models.Interfaces;
    using System.Text;

    public class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        private Corps corps;
        public SpecialisedSoldier(string id, string firstName, string lastName, decimal salary, string corps)
            : base(id, firstName, lastName, salary)
        {
            Corps = corps;
        }

        public string Corps 
        { 
            get=> this.corps.ToString();

            private set
            {
                Corps corps;

                if (!Enum.TryParse(value,out corps))
                {
                    throw new ArgumentException();
                }
                this.corps = corps;
            }
        }
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(base.ToString())
                .AppendLine($"Corps: {Corps}");

            return builder.ToString().TrimEnd();
        }
    }
}
