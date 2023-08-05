using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private readonly List<ILoan> loans;
        private readonly List<IClient> clients;

        public Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            loans = new List<ILoan>();
            clients = new List<IClient>();

        }

        private string name;
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                name = value;
            }
        }


        public int Capacity { get; private set; }

        public IReadOnlyCollection<ILoan> Loans => loans.AsReadOnly();

        public IReadOnlyCollection<IClient> Clients => clients.AsReadOnly();

        public void AddClient(IClient Client)
        {
            if (Capacity <= clients.Count)
            {
                throw new ArgumentException("Not enough capacity for this client.");
            }
            clients.Add(Client);
        }

        public void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();

            List<string> allClients = new List<string>();
            string Clients = "";

            sb.AppendLine($"Name: {Name}, Type: {this.GetType().Name}");

            if (clients.Any())
            {
                foreach (var client in clients)
                {
                    allClients.Add(client.Name);
                }
            }

            Clients = allClients.Any() ? string.Join(", ", allClients) : "none";

            sb.AppendLine($"Clients: {Clients}");

            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {SumRates()}");

            return sb.ToString().Trim();
        }

        public void RemoveClient(IClient Client)
        {
            clients.Remove(Client);
        }

        public double SumRates()
        {
            double sumOfAllInterest = 0;

            foreach (var loan in loans)
            {
                sumOfAllInterest += loan.InterestRate;
            }

            return sumOfAllInterest;
        }
    }
}
