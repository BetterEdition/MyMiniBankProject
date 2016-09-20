using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BE;
using Interfaces;
using BLL;
using Repositories;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class BankAccountControllerTest
    {
        private IRepository<IBankAccount, int> repository = null;

        [TestInitialize]
        public void init_Test()
        {
            repository = new BankAccountDBRepository();
        }

        [TestMethod]
        public void CreateNewBankAccount_For_Customer_Test()
        {
           // IRepository<IBankAccount, int> repository = new BankAccountRepository();
            BankAccountController mgr = new BankAccountController(repository);

            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");
            double initialBalance = 123.45;

            IBankAccount account = mgr.CreateNewBankAccount(customer, initialBalance);

            Assert.AreEqual(1, repository.Count);
            IBankAccount repAccount = repository.GetAll()[0];

            Assert.AreSame(account, repAccount);

            Assert.AreEqual(1, account.AccountNumber);
            Assert.AreEqual(initialBalance, account.Balance);

            Assert.AreEqual(1, account.Owners.Count);
            Assert.AreSame(customer, account.Owners[0]);

            Assert.AreEqual(1, customer.BankAccounts.Count);
            Assert.AreSame(account, customer.BankAccounts[0]);
        }

        [TestMethod]
        public void AddOwnerToBankAccount_New_Owner_Test()
        {
            //IRepository<IBankAccount, int> repository = new BankAccountRepository();
            BankAccountController mgr = new BankAccountController(repository);

            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(2, "Name", "Address", "Phone", "Email");

            IBankAccount account1 = mgr.CreateNewBankAccount(customer1);
            IBankAccount account2 = mgr.CreateNewBankAccount(customer2);

            mgr.AddOwnerToBankAccount(account1, customer2);

            Assert.AreEqual(2, account1.Owners.Count);
            Assert.AreSame(customer1, account1.Owners[0]);
            Assert.AreSame(customer2, account1.Owners[1]);

            Assert.AreEqual(2, customer2.BankAccounts.Count);
            Assert.AreSame(account2, customer2.BankAccounts[0]);
            Assert.AreSame(account1, customer2.BankAccounts[1]);
        }

        [TestMethod]
        public void AddOwnerToBankAccount_Existing_Owner_Expect_ArgumentException_Test()
        {
            //IRepository<IBankAccount, int> repository = new BankAccountRepository();
            BankAccountController mgr = new BankAccountController(repository);

            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");

            IBankAccount account1 = mgr.CreateNewBankAccount(customer1);

            Assert.AreEqual(1, account1.Owners.Count);
            Assert.AreSame(customer1, account1.Owners[0]);

            Assert.AreEqual(1, customer1.BankAccounts.Count);
            Assert.AreSame(account1, customer1.BankAccounts[0]);

            try
            {
                mgr.AddOwnerToBankAccount(account1, customer1);
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, account1.Owners.Count);
                Assert.AreSame(customer1, account1.Owners[0]);

                Assert.AreEqual(1, customer1.BankAccounts.Count);
                Assert.AreSame(account1, customer1.BankAccounts[0]);
            }
        }
        [TestMethod]
        public void AddOwnerToBankAccount_Null_Owner_Expect_ArgumentNullException_Test()
        {
            //IRepository<IBankAccount, int> repository = new BankAccountRepository();
            BankAccountController mgr = new BankAccountController(repository);

            ICustomer customer1 = null;
            IBankAccount account1 = null;

            try
            {
                account1 = mgr.CreateNewBankAccount(customer1);
                Assert.Fail("Created bank account with NULL owner");
            }
            catch (ArgumentNullException)
            {
                Assert.IsNull(account1);
            }
        }

        [TestMethod]
        public void RemoveOwnerFromBankAccount_Existing_Owner_Test()
        {
            //IRepository<IBankAccount, int> repository = new BankAccountRepository();
            BankAccountController mgr = new BankAccountController(repository);

            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(2, "Name", "Address", "Phone", "Email");

            IBankAccount account1 = mgr.CreateNewBankAccount(customer1);
            IBankAccount account2 = mgr.CreateNewBankAccount(customer2);
            mgr.AddOwnerToBankAccount(account1, customer2);

            mgr.RemoveOwnerFromBankAccount(account1, customer2);

            Assert.AreEqual(1, account1.Owners.Count);
            Assert.AreSame(customer1, account1.Owners[0]);
        }

        [TestMethod]
        public void RemoveOwnerFromBankAccount_NonExisting_Owner_Expect_ArgumentException_Test()
        {
            //IRepository<IBankAccount, int> repository = new BankAccountRepository();
            BankAccountController mgr = new BankAccountController(repository);

            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(2, "Name", "Address", "Phone", "Email");

            IBankAccount account1 = mgr.CreateNewBankAccount(customer1);

            try
            {
                mgr.RemoveOwnerFromBankAccount(account1, customer2);
                Assert.Fail("removed non-existing owner from bank account.");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, account1.Owners.Count);
                Assert.AreSame(customer1, account1.Owners[0]);

                Assert.AreEqual(1, customer1.BankAccounts.Count);
                Assert.AreSame(account1, customer1.BankAccounts[0]);
            }
        }

        [TestMethod]
        public void RemoveOwnerFromBankAccount_NULL_Owner_Expect_ArgumentNullException_Test()
        {
            //IRepository<IBankAccount, int> repository = new BankAccountRepository();
            BankAccountController mgr = new BankAccountController(repository);

            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = null;

            IBankAccount account1 = mgr.CreateNewBankAccount(customer1);

            try
            {
                mgr.RemoveOwnerFromBankAccount(account1, customer2);
                Assert.Fail("removed NULL owner from bank account.");
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(1, account1.Owners.Count);
                Assert.AreSame(customer1, account1.Owners[0]);

                Assert.AreEqual(1, customer1.BankAccounts.Count);
                Assert.AreSame(account1, customer1.BankAccounts[0]);
            }
        }

        [TestMethod]
        public void GetAllBankAccounts_Test()
        {
            //IRepository<IBankAccount, int> repository = new BankAccountRepository();
            BankAccountController mgr = new BankAccountController(repository);

            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(2, "Name", "Address", "Phone", "Email");

            IBankAccount account1 = mgr.CreateNewBankAccount(customer1);
            IBankAccount account2 = mgr.CreateNewBankAccount(customer2);

            IList<IBankAccount> returned = mgr.GetAllBankAccounts();
            Assert.AreEqual(2, returned.Count);
            Assert.AreSame(account1, returned[0]);
            Assert.AreSame(account2, returned[1]);
        }

        [TestMethod]
        public void GetById_Existing_Account_Test()
        {
            //IRepository<IBankAccount, int> repository = new BankAccountRepository();
            BankAccountController mgr = new BankAccountController(repository);

            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(2, "Name", "Address", "Phone", "Email");

            IBankAccount account1 = mgr.CreateNewBankAccount(customer1);
            IBankAccount account2 = mgr.CreateNewBankAccount(customer2);

            IBankAccount result = mgr.GetBankAccountById(account2.AccountNumber);

            Assert.IsNotNull(result);
            Assert.AreSame(account2, result);
        }


        [TestMethod]
        public void GetById_Non_Existing_Account_Expect_Null_Test()
        {
            //IRepository<IBankAccount, int> repository = new BankAccountRepository();
            BankAccountController mgr = new BankAccountController(repository);

            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");

            IBankAccount account1 = mgr.CreateNewBankAccount(customer1);

            IBankAccount result = mgr.GetBankAccountById(2);

            Assert.IsNull(result);
        }
    }
}
