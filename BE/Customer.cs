﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Customer : ICustomer
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Addresss { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IList<IBankAccount> BankAccounts { get; set; }

        public Customer(int id, string name, string addresss, string phone, string email)
        {
            Id = id;
            Name = name;
            Addresss = addresss;
            Phone = phone;
            Email = email;
            BankAccounts = new List<IBankAccount>();
        }

        public void AddBankAccount(IBankAccount account)
        {
            if (BankAccounts.Contains(account))
            {
                throw new ArgumentException("Bank Account already exists for customer");
            }
            BankAccounts.Add(account);
        }

        public void RemoveBankAccount(IBankAccount account)
        {
            if (! BankAccounts.Contains(account))
            {
                throw new ArgumentException("Bank Account does not exist");
            }
            int index = BankAccounts.IndexOf(account);
            if (BankAccounts[index].Balance > 0)
                throw new ArgumentException("Cannot remove non-empty bank account from customer");

            BankAccounts.RemoveAt(index);
        }
    }
}
