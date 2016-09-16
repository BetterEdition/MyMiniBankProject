using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ICustomer
    {
        int Id { get; }
        String Name { get; set;  }
        String Address { get; set; }
        String Phone { get; set; }
        String Email { get; set; }
        IList<IBankAccount> BankAccounts { get; set; }

        void AddBankAccount(IBankAccount account);
        void RemoveBankAccount(IBankAccount account);
    }
}
