using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public class StudentLoan : Loan
    {
        private const int studentLoaninterestRate = 1;

        private const double studentLoanAmount = 10000;

        public StudentLoan() : base(studentLoaninterestRate, studentLoanAmount)
        {
        }
    }
}
