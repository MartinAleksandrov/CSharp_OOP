using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        private const int initialArmorThickness = 200;
        public Submarine(string name, double mainWeaponCaliber, double speed) :
            base(name, mainWeaponCaliber, speed, initialArmorThickness)
        {
            SubmergeMode = false;
        }

        public bool SubmergeMode { get; private set; } = false;

        public override void RepairVessel()
        {
            if (this.ArmorThickness < initialArmorThickness)
            {
                ArmorThickness = initialArmorThickness;
            }
        }
        public void ToggleSubmergeMode()
        {
            if (SubmergeMode)
            {
                this.MainWeaponCaliber -= 40;
                Speed += 4;
                SubmergeMode = false;
            }
            else
            {
                this.MainWeaponCaliber += 40;
                Speed -= 4;
                SubmergeMode = true;
            }
        }

        public override string ToString()
        {
            string OnOrOff = SubmergeMode ? "ON" : "OFF";

            return base.ToString() + Environment.NewLine + $" *Submerge mode: {OnOrOff}";
        }
    }
}
