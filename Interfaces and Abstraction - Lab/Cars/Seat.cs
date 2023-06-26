using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class Seat : Car
    {
        public Seat(string model, string color) : 
            base(model, color)
        {
        }

        public override string ToString()
        {
            return $"{Color} Seat {Model}";
        }
    }
}
