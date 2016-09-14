using BE;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BankAccountRepository : IRepository<IBankAccount, int>
    {
        protected IList<IBankAccount> bankAccounts = new List<IBankAccount>();
        public int Count
        {
            get
            {
                return bankAccounts.Count;
            }
        }

        public void Add(IBankAccount account)
        {
            if (bankAccounts.Contains(account))
            {
                throw new ArgumentException("Bank Account already exist");
            }
            bankAccounts.Add(account);
        }

        public IList<IBankAccount> GetAll()
        {
            return bankAccounts.ToList();
        }

        public IBankAccount GetById(int id)
        {
            return bankAccounts.Where(acc => acc.AccountNumber == id).SingleOrDefault();
        }

        public void Remove(IBankAccount account)
        {
            if (! bankAccounts.Contains(account))
            {
                throw new ArgumentException("Unable to remove bank account");
            }
            bankAccounts.Remove(account);
        }
    }
}
