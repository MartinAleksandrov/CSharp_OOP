using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Repositories
{
    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> races;

        public RaceRepository()
        {
            races = new List<IRace>();
        }

        public IReadOnlyCollection<IRace> Models => races.AsReadOnly();

        public void Add(IRace model) =>
            races.Add(model);

        public IRace FindByName(string name) =>
            races.FirstOrDefault(r => r.RaceName == name);

        public bool Remove(IRace model) =>
            races.Remove(model);
    }
}
