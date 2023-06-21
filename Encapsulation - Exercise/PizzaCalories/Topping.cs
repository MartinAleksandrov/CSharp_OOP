using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Topping
    {
        private const double baseToppingCal = 2.0;
        private string toppingType;
        private double topppingGrams;
        
        public Topping(string toppingType, double topppingGrams)
        {
            ToppingType = toppingType;
            TopppingGrams = topppingGrams;
        }

        public string ToppingType
        {
            get => toppingType;
            private set
            {
                if (DataTopping(value)==-1)
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                toppingType = value;
            }
        }
        public double TopppingGrams
        {
            get => topppingGrams;
            private set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{ToppingType} weight should be in the range [1..50].");
                }
                topppingGrams = value;
            }
        }

        public double TotalCalories { get; set; }
        public void CalOfTopping(string toppingType, double grams)
        {
            TotalCalories += (baseToppingCal * grams) * DataTopping(toppingType);
        }

        private double DataTopping(string toppingType)
        {
            switch (toppingType.ToLower())
            {
                case "meat":
                    return 1.2;
                case "veggies":
                    return 0.8;
                case "cheese":
                    return 1.1;
                case "sauce":
                    return 0.9;
                default:
                    break;
            }
            return -1;
        }

    }
}