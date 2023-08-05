using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private readonly LoanRepository loans;
        private readonly BankRepository banks;

        public Controller()
        {
            loans = new LoanRepository();
            banks = new BankRepository();
        }

        public string AddBank(string bankTypeName, string name)
        {
            IBank bank;
            if (bankTypeName != nameof(CentralBank) && bankTypeName != nameof(BranchBank))
            {
                throw new ArgumentException(ExceptionMessages.BankTypeInvalid);
            }
            else
            {
                if (bankTypeName == nameof(CentralBank))
                {
                    bank = new CentralBank(name);
                }
                else
                {
                    bank = new BranchBank(name);
                }

            }
            banks.AddModel(bank);

            return string.Format(OutputMessages.BankSuccessfullyAdded,bankTypeName);

        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            IClient client;
            if (clientTypeName != nameof(Student) && clientTypeName != nameof(Adult))
            {
                throw new ArgumentException(ExceptionMessages.ClientTypeInvalid);
            }
            IBank bank = banks.FirstModel(bankName);

            if (bank.GetType().Name == nameof(CentralBank) && clientTypeName == nameof(Adult))
            {
                client = new Adult(clientName,id,income);
            }
            else if (bank.GetType().Name == nameof(BranchBank) && clientTypeName == nameof(Student))
            {
                client = new Student(clientName, id, income);
            }
            else
            {
                return OutputMessages.UnsuitableBank;
            }

            bank.AddClient(client);
            return string.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName ,bankName);
        }

        public string AddLoan(string loanTypeName)
        {
            ILoan loan;
            if (loanTypeName != nameof(StudentLoan) && loanTypeName != nameof(MortgageLoan))
            {
                throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);
            }
            else
            {
                if (loanTypeName == nameof(StudentLoan))
                {
                    loan = new StudentLoan();
                }
                else
                {
                    loan = new MortgageLoan();
                }
            }

            loans.AddModel(loan);

            return string.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
        }

        public string FinalCalculation(string bankName)
        {
            IBank bank = banks.FirstModel(bankName);

            double allFund = 0;

            foreach (var client in bank.Clients)
            {
                allFund += client.Income;
            }
            foreach (var loan in bank.Loans)
            {
                allFund += loan.Amount;
            }

            return string.Format(OutputMessages.BankFundsCalculated,bankName,$"{allFund:f2}");
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            IBank bank = banks.FirstModel(bankName);

            ILoan loan = loans.FirstModel(loanTypeName);

            if (loan == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.MissingLoanFromType,loanTypeName));
            }


            bank.AddLoan(loan);
            loans.RemoveModel(loan);

            return string.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName , bankName);
        }

        public string Statistics()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var bank in banks.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }

            return sb.ToString().Trim();
        }
    }
}
