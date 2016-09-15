using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBankAccountController
    {
        IBankAccount CreateNewBankAccount(ICustomer owner, double initialBalance = 0.0);
        void RemoveBankAccount(IBankAccount acc);
        void AddOwnerToBankAccount(IBankAccount acc, ICustomer owner);
        void RemoveOwnerFromBankAccount(IBankAccount acc, ICustomer owner);
        IList<IBankAccount> GetAllBankAccounts();
        IBankAccount GetBankAccountById(int accNumber);
    }
}
