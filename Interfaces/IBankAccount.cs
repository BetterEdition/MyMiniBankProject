using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBankAccount
    {
       int AccountNumber { get; }
        double Balance { get; }
        double InterestRate { get; set; }

        IList<ITransaction> Transactions { get; }
        IList<ICustomer> Owners { get; }

        void Deposit(double amount);
        void Withdraw(double amount);
        void AddInterest();
        void AddOwner(ICustomer owner);
        void RemoveOwner(ICustomer owner);

        IList<ITransaction> GetTransactions(DateTime from, DateTime to);
        IList<ITransaction> GetTransactions(DateTime from);
    }
}
