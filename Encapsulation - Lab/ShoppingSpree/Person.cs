using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShoppingSpree
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> bag;

        public Person(string name, decimal money)
        {
            Name = name;
            Money = money;
            bag = new List<Product>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                name = value;
            }
        }

        public decimal Money
        {
            get => money;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                money = value;
            }
        }

        public string AddProduct(Product product)
        {
            if (Money >= product.Cost)
            {
                Money -= product.Cost;
                bag.Add(product);
                return $"{Name} bought {product}";
            }
            return $"{Name} can't afford {product}";
        }

        public override string ToString()
        {
            if (bag.Any())
            {
                string output = string.Join(", ", bag);
                return $"{Name} - {output}";
            }
            else
            {
                return $"{Name} - Nothing bought";
            }

        }
    }
}
