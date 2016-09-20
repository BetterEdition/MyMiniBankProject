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

        virtual public void Add(IBankAccount account)
        {
            if (bankAccounts.Contains(account))
            {
                throw new ArgumentException("Bank account already exist");
            }
            bankAccounts.Add(account);
        }

        public IList<IBankAccount> GetAll()
        {
            return bankAccounts.ToList();
        }

        public IBankAccount GetById(int id)
        {
            foreach (IBankAccount acc in bankAccounts)
                if (acc.AccountNumber == id)
                    return acc;
            return null;
        }

        public void Remove(IBankAccount account)
        {
            if (! bankAccounts.Contains(account))
            {
                throw new ArgumentException("Bank account does not exist.");
            }
            bankAccounts.Remove(account);
        }
    }
}
