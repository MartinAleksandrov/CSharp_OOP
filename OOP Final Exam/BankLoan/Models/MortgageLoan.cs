using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class MortgageLoan : Loan
    {
        private const int mortageLoanInterestRate = 3;
        private const double mortageLoanAmount = 50000;

        public MortgageLoan() : base(mortageLoanInterestRate, mortageLoanAmount)
        {
        }
    }
}
