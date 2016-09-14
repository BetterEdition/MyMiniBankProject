using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICustomer
    {
        int Id { get; }
        String Name { get; set;  }
        String Addresss { get; set; }
        String Phone { get; set; }
        String Email { get; set; }
        IList<IBankAccount> BankAccounts { get; set; }

        void AddBankAccount(IBankAccount account);
        void RemoveBankAccount(IBankAccount account);
    }
}
