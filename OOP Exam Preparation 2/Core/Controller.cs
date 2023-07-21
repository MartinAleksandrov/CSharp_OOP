using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Repositories.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;

        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }

        public string CreateRobot(string model, string typeName)
        {
            IRobot newRobot;

            if (typeName!= "DomesticAssistant" && typeName != "IndustrialAssistant")
            {
                return string.Format(OutputMessages.RobotCannotBeCreated,typeName);
            }
            else
            {
                if (typeName == "DomesticAssistant")
                {
                    newRobot = new DomesticAssistant(model);
                }
                else
                {
                    newRobot = new IndustrialAssistant(model);
                }
                robots.AddNew(newRobot);
            }
            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName,model);

        }

        public string CreateSupplement(string typeName)
        {
            ISupplement newSupplement;

            if (typeName != "SpecializedArm" && typeName != "LaserRadar")
            {
                return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }
            else
            {
                if (typeName == "SpecializedArm")
                {
                    newSupplement = new SpecializedArm();
                }
                else
                {
                    newSupplement = new LaserRadar();
                }
                supplements.AddNew(newSupplement);
            }
            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            var allRobots = robots.Models().Where(x=>x.InterfaceStandards.Any(y=> y == intefaceStandard)).OrderByDescending(b=>b.BatteryLevel);


            if (allRobots.Count() == 0)
            {
                return string.Format(OutputMessages.UnableToPerform,intefaceStandard);
            }

            int batterySum = allRobots.Sum(r => r.BatteryLevel);

            if (batterySum<totalPowerNeeded)
            {
                totalPowerNeeded -= batterySum;
                return string.Format(OutputMessages.MorePowerNeeded, serviceName,totalPowerNeeded);
            }
            else
            {
                int counter = 0;

                foreach (var robot in allRobots)
                {
                    counter++;
                    if (robot.BatteryLevel >= totalPowerNeeded)
                    {
                        robot.ExecuteService(totalPowerNeeded);
                        break;
                    }
                    else
                    {
                        totalPowerNeeded -= robot.BatteryLevel;
                        robot.ExecuteService(robot.BatteryLevel);
                    }
                }
                return string.Format(OutputMessages.PerformedSuccessfully,serviceName,counter);
            }
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var rob in robots.Models().OrderByDescending(r=>r.BatteryLevel).ThenBy(r=>r.BatteryCapacity))
            {
                sb.AppendLine(rob.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            int fedCount = 0;

            foreach (var rob in robots.Models().Where(r=>r.Model==model))
            {
                int totalBatteryCapacity = rob.BatteryCapacity;

                if ((totalBatteryCapacity/2) > rob.BatteryLevel)
                {
                    rob.Eating(minutes);
                    fedCount++;
                }
            }
            return string.Format(OutputMessages.RobotsFed,fedCount);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplement = supplements.Models().FirstOrDefault(s => s.GetType().Name == supplementTypeName);

            int interfaceValue = supplement.InterfaceStandard;


            var searchedRobots = robots.Models().Where(r => r.Model == model);
            var stillNotUpgradet = searchedRobots.Where(r => r.InterfaceStandards.All(s=>s != supplement.InterfaceStandard));
            var robotForUpgrade = stillNotUpgradet.FirstOrDefault();

            if (robotForUpgrade == null)
            {
                return string.Format(OutputMessages.AllModelsUpgraded,model);
            }
            else
            {
                robotForUpgrade.InstallSupplement(supplement);
                supplements.RemoveByName(supplementTypeName);
                return string.Format(OutputMessages.UpgradeSuccessful, model,supplementTypeName);
            }
        }
    }
}
