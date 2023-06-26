namespace Military_Elite.Classes
{
    using Models.Interfaces;
    using System.Collections.Generic;
    using System.Text;

    internal class LieutenantGeneral : Private, ILieutenantGeneral
    {
        private List<Private> privates;

        public LieutenantGeneral(string id, string firstName, string lastName, decimal salary) :
            base(id, firstName, lastName, salary)
        {
            privates = new List<Private>();
        }

        public IReadOnlyCollection<IPrivate> Privates { get; }

        public void AddPrivate(Private priv)
        {
            privates.Add(priv);
        }
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(base.ToString())
                .AppendLine("Privates:");

            foreach (var current in this.privates)
            {
                builder.AppendLine("  " + current.ToString());
            }

            return builder.ToString().TrimEnd();
        }
    }
}
