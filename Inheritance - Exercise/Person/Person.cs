using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    public class Person
    {
        private int age;
        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; set; }
        public virtual int Age 
        {
            get
            {
                return age;
            }
            set 
            {
                if (value>0)
                {
                    age = value;
                }
            }
        }
        

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Name: {this.Name}, Age: {this.Age}");

            return stringBuilder.ToString().Trim();
        }
    }
}
