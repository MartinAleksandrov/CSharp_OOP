using Military_Elite.Models.Interfaces;
using System.Text;

namespace Military_Elite.Classes
{
    internal class Spy : Soldier, ISpy
    {
        public Spy(string id, string firstName, string lastName, int codeNumber) : base(id, firstName, lastName)
        {
            CodeNumber = codeNumber;
        }

        public int CodeNumber { get; private set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(base.ToString())
                .Append($"Code Number: {CodeNumber}");

            return builder.ToString();
        }
    }
}
