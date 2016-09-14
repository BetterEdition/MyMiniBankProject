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

        void Deposit(double amount);
        void Withdraw(double amount);
        void AddInterest();
    }
}
