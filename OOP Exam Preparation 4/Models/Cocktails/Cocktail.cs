using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public class Cocktail : ICocktail
    {
        public Cocktail(string cocktailName, string size, double price)
        {
            Name = cocktailName;
            this.size = size;
            Price = price;
        }

        private string name;
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                name = value;
            }
        }


        private string size;
        public string Size
        {
            get { return size; }
        }


        private double price;
        public double Price
        {
            get { return price; }
            private set
            {
                double result;
                if (this.size == "Large")
                {
                    price = value;
                }
                else if (this.size == "Middle")
                {
                    result = (value * 2) / 3;
                    price = result;
                }
                else if (this.size == "Small")
                {
                    result = value / 3;
                    price = result;
                }
            }
        }

        public override string ToString()
        {
            return $"{this.name} ({this.size}) - {price:f2} lv";
        }
    }
}
