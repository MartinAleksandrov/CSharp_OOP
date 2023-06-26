﻿namespace Military_Elite.Classes
{
    using Models.Interfaces;
    public class Private : Soldier, IPrivate
    {
        public Private(string id, string firstName, string lastName, decimal salary) :
            base(id, firstName, lastName)
        {
            Salary = salary;
        }

        public decimal Salary { get; private set; }
        public override string ToString()
        {
            return $"{base.ToString()} Salary: {Salary:F2}";
        }
    }
}
