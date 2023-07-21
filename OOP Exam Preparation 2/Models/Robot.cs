using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        public Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            this.Model = model;
            this.BatteryCapacity = batteryCapacity;
            this.convertionCapacityIndex = conversionCapacityIndex;

            this.batteryLevel = batteryCapacity;
            this.interfaceStandardsCollcetion = new List<int>();
        }
            
        private string model;
        public string Model
        {
            get { return model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNullOrWhitespace);
                }

                model = value;
            }
        }


        private int batteryCapacity;
        public int BatteryCapacity
        {
            get { return batteryCapacity; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
                }

                batteryCapacity = value;
            }
        }


        private int batteryLevel;
        public int BatteryLevel => this.batteryLevel;


        private int convertionCapacityIndex;
        public int ConvertionCapacityIndex
        {
            get { return convertionCapacityIndex; }
            private set
            {
                convertionCapacityIndex = value;
            }
        }


        private List<int> interfaceStandardsCollcetion;
        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandardsCollcetion;

        public void Eating(int minutes)
        {
            int producedEnergy = convertionCapacityIndex * minutes;

            if (producedEnergy + batteryLevel >= batteryCapacity)
            {
                batteryLevel = batteryCapacity;
            }
            else
            {
                batteryLevel += producedEnergy;
            }
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (batteryLevel >= consumedEnergy)
            {
                this.batteryLevel -= consumedEnergy;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void InstallSupplement(ISupplement supplement)
        {
            this.batteryCapacity -= supplement.BatteryUsage;
            this.batteryLevel -= supplement.BatteryUsage;
            interfaceStandardsCollcetion.Add(supplement.InterfaceStandard);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name} {Model}:");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");

            if (interfaceStandardsCollcetion.Count==0)
            {
               sb.AppendLine("--Supplements installed: none");
            }
            else
            {
                sb.AppendLine($"--Supplements installed: {string.Join(" ",InterfaceStandards)}");
            }

            return sb.ToString().Trim();
        }
    }
}
