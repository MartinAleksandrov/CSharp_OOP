using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class Adult : Client
    {
        private const int initialInterestOfAdult = 4;

        public Adult(string name, string id, double income) : base(name, id, initialInterestOfAdult, income)
        {
        }

        public override void IncreaseInterest()
        {
            Interest += 2;
        }
    }
}
