using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private readonly List<ILoan> loans;

        public LoanRepository()
        {
            loans = new List<ILoan>();
        }

        public IReadOnlyCollection<ILoan> Models => loans.AsReadOnly();

        public void AddModel(ILoan model) =>
            loans.Add(model);

        public ILoan FirstModel(string name) =>
            loans.FirstOrDefault(l=> l.GetType().Name == name);

        public bool RemoveModel(ILoan model) =>
            loans.Remove(model);
    }
}
