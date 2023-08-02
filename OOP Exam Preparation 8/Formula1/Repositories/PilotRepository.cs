using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<IPilot>
    {
        private readonly List<IPilot> pilots;

        public PilotRepository()
        {
            pilots = new List<IPilot>();
        }

        public IReadOnlyCollection<IPilot> Models => pilots.AsReadOnly();

        public void Add(IPilot model) =>
            pilots.Add(model);

        public IPilot FindByName(string name) =>
            pilots.FirstOrDefault(p => p.FullName == name);

        public bool Remove(IPilot model) =>
            pilots.Remove(model);
    }
}
