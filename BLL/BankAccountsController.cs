using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BankAccountsController
    {
        private IRepository<IBankAccount, int> accounts;

        public BankAccountsController(IRepository<IBankAccount, int> repository)
        {
            accounts = repository;
        }

        public void AddBankAccount(IBankAccount acc, ICustomer customer)
        {

        }

        public void RemoveBankAccount(IBankAccount acc)
        {

        }
    }
}
