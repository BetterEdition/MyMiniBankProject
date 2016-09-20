using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BE;
using Repositories;
using Interfaces;
using System.Collections.Generic;
using Moq;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class BankAccountRepositoryTest
    {
        private static Mock<IRepository<IBankAccount, int>> mock = null;
        private static IList<IBankAccount> items = null;

        [ClassInitialize]
        public static void classSetup(TestContext context)
        {
            mock = new Mock<IRepository<IBankAccount, int>>();

            mock.Setup(x => x.GetAll()).Returns(() => items);
            mock.SetupGet(x => x.Count).Returns(() => items.Count);
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int i) => items.Where((acc) => acc.AccountNumber == i).SingleOrDefault());
            mock.Setup(x => x.Add(It.IsAny<IBankAccount>())).Callback<IBankAccount>((a) =>
            {
                if (items.Contains(a))
                    throw new ArgumentException();
                else
                    items.Add(a);
            });
            mock.Setup(x => x.Remove(It.IsAny<IBankAccount>())).Callback<IBankAccount>((a) =>
            {
                if (items.Contains(a))
                    items.Remove(a);
                else
                    throw new ArgumentException();
            });
        }

        [TestInitialize]
        public void TestInitialize()
        {
            items = new List<IBankAccount>();
        }

        [TestMethod]
        public void Create_BankAccountRepository_Test()
        {
            IRepository<IBankAccount, int> accounts = mock.Object;

            Assert.IsNotNull(accounts);
            Assert.AreEqual(0, accounts.Count);
        }

        [TestMethod]
        public void Add_New_BankAccount_Test()
        {
            IRepository<IBankAccount, int> accounts = mock.Object;

            int accNumber = 1;
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber);

            accounts.Add(account);

            Assert.AreEqual(1, items.Count);
            Assert.AreSame(account, accounts.GetById(accNumber));
        }

        [TestMethod]
        public void Add_Existing_BankAccount_Expect_ArgumentException_Test()
        {
            IRepository<IBankAccount, int> accounts = mock.Object;

            int accNumber = 1;
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber);

            accounts.Add(account);
            int counter = accounts.Count;
            Assert.AreEqual(1, counter);

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
            IRepository<IBankAccount, int> accounts = mock.Object;

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
            IRepository<IBankAccount, int> accounts = mock.Object;

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
            IRepository<IBankAccount, int> accounts = mock.Object;
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
            IRepository<IBankAccount, int> accounts = mock.Object;

            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");
            IBankAccount account1 = BankAccount.CreateBankAccount(customer, 1);

            accounts.Add(account1);

            IBankAccount returned = accounts.GetById(2);

            Assert.IsNull(returned);
        }

        [TestMethod]
        public void GetAll_Test()
        {
            IRepository<IBankAccount, int> accounts = mock.Object;
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
