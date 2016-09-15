using BE;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BankAccountController : IBankAccountController
    {
        private int nextAccountNumber = 1;
        private IRepository<IBankAccount, int> accounts;

        public BankAccountController(IRepository<IBankAccount, int> repository)
        {
            accounts = repository;
        }

        public IBankAccount CreateNewBankAccount(ICustomer owner, double initialBalance = 0.0)
        {
            IBankAccount account = BankAccount.CreateBankAccount(owner, nextAccountNumber++, initialBalance);
            accounts.Add(account);
            return account;
        }

        public void AddOwnerToBankAccount(IBankAccount acc, ICustomer owner)
        {
            if (owner.BankAccounts.Contains(acc))
            {
                throw new ArgumentException("Owner already registered for bank account");
            }
            acc.AddOwner(owner);
        }

        public void RemoveOwnerFromBankAccount(IBankAccount acc, ICustomer owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("No owner specified.");
            }
            if (!owner.BankAccounts.Contains(acc))
            {
                throw new ArgumentException("Owner not registered for bank account");
            }
            acc.RemoveOwner(owner);
        }

        public IList<IBankAccount> GetAllBankAccounts()
        {
            return accounts.GetAll();
        }

        public IBankAccount GetBankAccountById(int accNumber)
        {
            return accounts.GetById(accNumber);
        }

        public void RemoveBankAccount(IBankAccount acc)
        {
            if (acc.Balance > 0)
            {
                throw new ArgumentException("Cannot remove non-empty Bank Account");
            }
            foreach (ICustomer customer in acc.Owners)
            {
                customer.BankAccounts.Remove(acc);
            }
            accounts.Remove(acc);
        }
    }
}
