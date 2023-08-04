using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Captain : ICaptain
    {
        private readonly List<IVessel> vessels;

        public Captain(string fullName)
        {
            vessels = new List<IVessel>();
            FullName = fullName;
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainName);
                }
                fullName = value;
            }
        }


        public int CombatExperience { get; private set; } = 0;

        public ICollection<IVessel> Vessels => vessels.AsReadOnly();

        public void AddVessel(IVessel vessel)
        {
            if (vessel == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidVesselForCaptain);
            }
            vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            CombatExperience += 10;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            if (vessels.Any())
            {
               sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {vessels.Count} vessels.");

                foreach (var ves in vessels)
                {
                    sb.AppendLine(ves.ToString());
                }

                return sb.ToString().Trim();
            }
            return $"{FullName} has {CombatExperience} combat experience and commands {vessels.Count} vessels.";
        }
    }
}
