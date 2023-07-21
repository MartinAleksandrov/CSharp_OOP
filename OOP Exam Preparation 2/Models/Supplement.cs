using RobotService.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Supplement : ISupplement
    {

        public Supplement(int interfaceStandard, int batteryUsage)
        {
            this.InterfaceStandard = interfaceStandard;
            this.BatteryUsage = batteryUsage;
        }

        private int interfaceStandard;

        public int InterfaceStandard
        {
            get { return interfaceStandard; }
            private set 
            { 
                interfaceStandard = value; 
            }
        }

        private int batteryUsage;

        public int BatteryUsage
        {
            get { return batteryUsage; }
            private set
            {
                batteryUsage = value;
            }
        }
    }
}
