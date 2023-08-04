using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private const int initialArmorThickness = 300;
        public Battleship(string name, double mainWeaponCaliber, double speed) : 
            base(name, mainWeaponCaliber, speed, initialArmorThickness)
        {
            SonarMode = false;
        }

        public bool SonarMode { get; private set; }

        public override void RepairVessel()
        {
            if (this.ArmorThickness < initialArmorThickness)
            {
                ArmorThickness = initialArmorThickness;
            }
        }
        public void ToggleSonarMode()
        {
            if (SonarMode)
            {
                this.MainWeaponCaliber -= 40;
                Speed += 5;
                SonarMode = false;
            }
            else
            {
                this.MainWeaponCaliber += 40;
                Speed -= 5;
                SonarMode = true;
            }
        }

        public override string ToString()
        {
            string OnOrOff = SonarMode ? "ON" : "OFF";

            return base.ToString() + Environment.NewLine + $" *Sonar mode: {OnOrOff}";
        }
    }
}
