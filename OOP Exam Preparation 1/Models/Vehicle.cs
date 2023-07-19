using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private int batteryLevel;
        private bool isDamaged;
        private double maxMileage;

        public Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            this.Brand = brand;
            this.Model = model;
            this.maxMileage = maxMileage;
            this.LicensePlateNumber = licensePlateNumber;
            this.isDamaged = false;
            this.batteryLevel = 100;
        }

        private string brand;
        public string Brand
        {
            get { return brand; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BrandNull);
                }
                brand = value;
            }
        }


        private string model;
        public string Model
        {
            get { return model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNull);
                }
                model = value;
            }
        }


        private string licensePlateNumber;
        public string LicensePlateNumber
        {
            get { return licensePlateNumber; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
                }
                licensePlateNumber = value;
            }
        }

        public double MaxMileage => this.maxMileage;

        public int BatteryLevel => this.batteryLevel;

        public bool IsDamaged => this.isDamaged;

        public void ChangeStatus()
        {
            if (IsDamaged)
            {
                isDamaged = false;
            }
            else
            {
                isDamaged = true;
            }
        }

        public void Drive(double mileage)
        {
            //Percentage = (Number / Total) * 100 PercentageFormula

            double percentage = Math.Round((mileage / this.maxMileage) * 100);
            this.batteryLevel -= (int)percentage;

            if (this.GetType().Name == nameof(CargoVan))
            {
                this.batteryLevel -= 5;
            }
        }
        public void Recharge()
        {
            batteryLevel = 100;
        }


        public override string ToString()
        {
            string status = "OK";
            if (this.IsDamaged)
            {
                status = "damaged";
            }

            return $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: {status}";
        }
    }
}
