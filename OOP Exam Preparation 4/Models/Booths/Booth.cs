using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private IRepository<IDelicacy> delicacyMenu;
        private IRepository<ICocktail> cocktailMenu;
        private double currentBill;
        private double turnover;
        private bool isReserved;


        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            delicacyMenu = new DelicacyRepository();
            cocktailMenu = new CocktailRepository();
            currentBill = 0;
            turnover = 0;
            isReserved = false;
        }

        public int BoothId
        {
            get { return boothId; }
            private set 
            {
                boothId = value; 
            }
        }
        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if (value<=0)
                {
                    throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                }
                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu
        {
            get { return delicacyMenu; }
            private set
            {
                delicacyMenu = value;
            }
        }
        public IRepository<ICocktail> CocktailMenu
        {
            get { return cocktailMenu; }
            private set
            {
                cocktailMenu = value;
            }
        }

        public double CurrentBill
        {
            get { return currentBill; }
     
        }
        public double Turnover
        {
            get { return turnover; }
        }

        public bool IsReserved
        {
            get { return isReserved; }
        }


        public void ChangeStatus()
        {
            if (isReserved)
            {
                isReserved = false;
            }
            else
            {
                isReserved = true;
            }
        }

        public void Charge()
        {
            this.turnover += this.currentBill;
            this.currentBill = 0;
        }

        public void UpdateCurrentBill(double amount)
        {
            this.currentBill += amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {this.boothId}");
            sb.AppendLine($"Capacity: {this.capacity}");
            sb.AppendLine($"Turnover: {this.turnover:f2} lv");

            sb.AppendLine($"-Cocktail menu:");
            foreach (var cock in cocktailMenu.Models)
            {
                sb.AppendLine($"--{cock}");
            }

            sb.AppendLine("-Delicacy menu:");
            foreach (var delicacy in delicacyMenu.Models)
            {
                sb.AppendLine($"--{delicacy}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
