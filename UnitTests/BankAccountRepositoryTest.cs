using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BE;
using Repositories;
using Interfaces;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class BankAccountRepositoryTest
    {
        [TestMethod]
        public void Create_BankAccountRepository_Test()
        {
            IRepository<IBankAccount, int> accounts = new BankAccountRepository();
            Assert.IsNotNull(accounts);
            Assert.AreEqual(0, accounts.Count);
        }

        [TestMethod]
        public void Add_New_BankAccount_Test()
        {
            IRepository<IBankAccount, int> accounts = new BankAccountRepository();

            int accNumber = 1;
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber);

            accounts.Add(account);

            Assert.AreEqual(1, accounts.Count);
            Assert.AreSame(account, accounts.GetById(accNumber));
        }

        [TestMethod]
        public void Add_Existing_BankAccount_Expect_ArgumentException_Test()
        {
            IRepository<IBankAccount, int> accounts = new BankAccountRepository();
            int accNumber = 1;
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber);

            accounts.Add(account);

           try
            {
                accounts.Add(account);
                Assert.Fail("Added existing bank account to repository");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, accounts.Count);
                Assert.AreSame(account, accounts.GetById(accNumber));
            }
        }

        [TestMethod]
        public void Remove_Existing_BankAccount_Test()
        {
            IRepository<IBankAccount, int> accounts = new BankAccountRepository();
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");
            IBankAccount account1 = BankAccount.CreateBankAccount(customer, 1);
            IBankAccount account2 = BankAccount.CreateBankAccount(customer, 2);

            accounts.Add(account1);
            accounts.Add(account2);

            accounts.Remove(account1);
            Assert.AreEqual(1, accounts.Count);
            Assert.AreSame(account2, accounts.GetAll()[0]);
        }

        [TestMethod]
        public void Remove_NonExisting_BankAccount_Expect_ArgumentException_Test()
        {
            IRepository<IBankAccount, int> accounts = new BankAccountRepository();
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");
            IBankAccount account1 = BankAccount.CreateBankAccount(customer, 1);
            IBankAccount account2 = BankAccount.CreateBankAccount(customer, 2);

            accounts.Add(account1);

            try
            {
                accounts.Remove(account2);
                Assert.Fail("Removed non-existing bank account");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, accounts.Count);
                Assert.AreSame(account1, accounts.GetAll()[0]);
            }
        }

        [TestMethod]
        public void GetById_Existing_BankAccount_Test()
        {
            IRepository<IBankAccount, int> accounts = new BankAccountRepository();
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");
            IBankAccount account1 = BankAccount.CreateBankAccount(customer, 1);
            IBankAccount account2 = BankAccount.CreateBankAccount(customer, 2);


            accounts.Add(account1);
            accounts.Add(account2);

            IBankAccount returned = accounts.GetById(2);
            Assert.IsNotNull(returned);
            Assert.AreSame(account2, returned);
        }

        [TestMethod]
        public void GetById_NonExisting_BankAccount_ExpectNull_Test()
        {
            IRepository<IBankAccount, int> accounts = new BankAccountRepository();
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");
            IBankAccount account1 = BankAccount.CreateBankAccount(customer, 1);

            accounts.Add(account1);

            IBankAccount returned = accounts.GetById(2);

            Assert.IsNull(returned);
        }

        [TestMethod]
        public void GetAll_Test()
        {
            IRepository<IBankAccount, int> accounts = new BankAccountRepository();
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");
            IBankAccount account1 = BankAccount.CreateBankAccount(customer, 1);
            IBankAccount account2 = BankAccount.CreateBankAccount(customer, 2);

            accounts.Add(account1);
            accounts.Add(account2);

            IList<IBankAccount> returned = accounts.GetAll();

            Assert.AreEqual(2, returned.Count);
            Assert.AreSame(account1, returned[0]);
            Assert.AreSame(account2, returned[1]);
        }
    }
}
