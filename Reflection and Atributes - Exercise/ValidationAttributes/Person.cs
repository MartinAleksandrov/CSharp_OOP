using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationAttributes
{
    public class Person
    {
        public Person(string fName, int age)
        {
            FullName = fName;
            Age = age;
        }

        [MyRequired]
        public string FullName { get; private set; }


        [MyRange(12,90)]
        public int Age { get; private set; }
    }
}
