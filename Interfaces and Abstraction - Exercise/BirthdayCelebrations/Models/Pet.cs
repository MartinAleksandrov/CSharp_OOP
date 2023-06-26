using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthdayCelebrations.Models.Interfaces;

namespace BirthdayCelebrations.Models
{
    public class Pet : IPet
    {
        public Pet(string name,string birthDate)
        {
            Name = name;
            Birthday = birthDate;

        }

        public string Name { get; private set; }

        public string Birthday { get; private set; }
    }
}
