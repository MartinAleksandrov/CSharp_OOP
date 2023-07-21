using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private List<IRobot> robots;
        public RobotRepository()
        {
            this.robots = new List<IRobot>();
        }

        public void AddNew(IRobot model) => robots.Add(model);

        public IRobot FindByStandard(int interfaceStandard) => robots.FirstOrDefault(r => r.InterfaceStandards.Any(x => x == interfaceStandard));

        public IReadOnlyCollection<IRobot> Models() => robots.AsReadOnly();

        public bool RemoveByName(string typeName) => robots.Remove(robots.FirstOrDefault(s => s.GetType().Name == typeName));
    }
}
