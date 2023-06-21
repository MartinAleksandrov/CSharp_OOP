using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsInfo
{
    public class Person
    {
        private string firstname;
        private string lastname;
        private int age;
        private decimal salary;

        public Person(string firstname, string lastname, int age,decimal salary)
        {
            FirstName = firstname;
            LastName = lastname;
            Age = age;
            Salary = salary;
        }

        public string FirstName
        {
            get { return firstname; }
            private set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException("First name cannot contain fewer than 3 symbols!");
                }
                firstname = value;
            }
        }
        public string LastName 
        {
            get { return lastname; }
            private set
            {
                if (value.Length<3)
                {
                    throw new ArgumentException("Last name cannot contain fewer than 3 symbols!");
                }
                lastname = value;
            }
        }
        public decimal Salary
        {
            get { return salary; }
            private set
            {
                if (value<650)
                {
                    throw new ArgumentException("Salary cannot be less than 650 leva!");
                }
                salary = value;
            }
            
        }
        public int Age
        {
            get { return age; }
            private set
            {
                if (value<=0)
                {
                    throw new ArgumentException("Age cannot be zero or a negative integer!");
                }
                age = value;
            }
        }

        public void IncreaseSalary(decimal percentage)
        {
            decimal incerease  = Salary * percentage /100;
 
            if (age < 30)
            {
                incerease /= 2;
            }
            Salary += incerease;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} receives {Salary:f2} leva.";
        }
    }
}