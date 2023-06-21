using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Dough
    {
        private const double baseDoughCal = 2.0;
        private string flourType;
        private string bakingTechnique;
        private double grams;

        public Dough(string flourType, string bakingTechnique, double grams)
        {
            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Grams = grams;
        }

        public string FlourType
        {
            get => flourType;
            private set
            {
                if (DataDough(value) == -1)
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                flourType = value;
            }
        }
        public string BakingTechnique
        {
            get => bakingTechnique;
            private set
            {
                if (DataDough(value) == -1)
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                bakingTechnique = value;
            }
        }
        public double Grams
        {
            get => grams;
            private set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                grams = value;
            }
        }


        public double CalOfDough(string flourType, string bakingTechnique, double grams)
        {
            return (baseDoughCal * grams) * DataDough(flourType) * DataDough(bakingTechnique);
        }

        private double DataDough(string flourTypeOrBakingTechnique)
        {
            switch (flourTypeOrBakingTechnique.ToLower())
            {
                case "white":
                    return 1.5;
                case "wholegrain":
                    return 1.0;
                case "crispy":
                    return 0.9;
                case "chewy":
                    return 1.1;
                case "homemade":
                    return 1.0;
                default:
                    break;
            }
            return -1;
        }

    }
}
