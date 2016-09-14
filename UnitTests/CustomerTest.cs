using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using BE;

namespace UnitTests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void CreateCustomer()
        {
            int id = 1;
            String name = "Some Name";
            String addresss = "Some Address";
            String phone = "Some Phone";
            String email = "Some Email";

            ICustomer customer = new Customer(id, name, addresss, phone, email);
            Assert.IsNotNull(customer);
            Assert.AreEqual(id, customer.Id);
            Assert.AreEqual(name, customer.Name);
            Assert.AreEqual(addresss, customer.Addresss);
            Assert.AreEqual(phone, customer.Phone);
            Assert.AreEqual(email, customer.Email);
            Assert.IsNotNull(customer.BankAccounts);
            Assert.AreEqual(0, customer.BankAccounts.Count);
        }

        [TestMethod]
        public void AddNewBankAccountToCustomer()
        {
            int id = 1;
            String name = "Some Name";
            String addresss = "Some Address";
            String phone = "Some Phone";
            String email = "Some Email";
            ICustomer customer = new Customer(id, name, addresss, phone, email);

            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = new BankAccount(accNumber, initialBalance);

            customer.AddBankAccount(account);
            Assert.AreEqual(1, customer.BankAccounts.Count);
            Assert.AreSame(account, customer.BankAccounts[0]);
        }


        [TestMethod]
        public void AddExistingBankAccountToCustomerExpectArgumentException()
        {
            int id = 1;
            String name = "Some Name";
            String addresss = "Some Address";
            String phone = "Some Phone";
            String email = "Some Email";
            ICustomer customer = new Customer(id, name, addresss, phone, email);

            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = new BankAccount(accNumber, initialBalance);

            customer.AddBankAccount(account);
            Assert.AreEqual(1, customer.BankAccounts.Count);
            Assert.AreSame(account, customer.BankAccounts[0]);

            try
            {
                customer.AddBankAccount(account);
                Assert.Fail("Added alrady existing bank account to customer");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, customer.BankAccounts.Count);
                Assert.AreSame(account, customer.BankAccounts[0]);
            }
        }

        [TestMethod]
        public void RemoveExistingBankAccountFromCustomer()
        {
            int id = 1;
            String name = "Some Name";
            String addresss = "Some Address";
            String phone = "Some Phone";
            String email = "Some Email";
            ICustomer customer = new Customer(id, name, addresss, phone, email);

            IBankAccount account1 = new BankAccount(1);
            IBankAccount account2 = new BankAccount(2);

            customer.AddBankAccount(account1);
            customer.AddBankAccount(account2);
            Assert.AreEqual(2, customer.BankAccounts.Count);
            Assert.AreSame(account1, customer.BankAccounts[0]);
            Assert.AreSame(account2, customer.BankAccounts[1]);

            customer.RemoveBankAccount(account1);

            Assert.AreEqual(1, customer.BankAccounts.Count);
            Assert.AreSame(account2, customer.BankAccounts[0]);
        }

        [TestMethod]

        public void RemoveNonExistingBankAccountFromCustomerExpectArgumentException()
        {
            int id = 1;
            String name = "Some Name";
            String addresss = "Some Address";
            String phone = "Some Phone";
            String email = "Some Email";
            ICustomer customer = new Customer(id, name, addresss, phone, email);

            IBankAccount account1 = new BankAccount(1);
            IBankAccount account2 = new BankAccount(2);

            customer.AddBankAccount(account1);
            Assert.AreEqual(1, customer.BankAccounts.Count);
            Assert.AreSame(account1, customer.BankAccounts[0]);

            try
            {
                customer.RemoveBankAccount(account2);
                Assert.Fail("Removed non-existing bank account from customer");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, customer.BankAccounts.Count);
                Assert.AreSame(account1, customer.BankAccounts[0]);
            }
        }

        [TestMethod]
        public void RemoveNonEmptyBankAccountFromCustomerExpectArgumentException()
        {
            int id = 1;
            String name = "Some Name";
            String addresss = "Some Address";
            String phone = "Some Phone";
            String email = "Some Email";
            ICustomer customer = new Customer(id, name, addresss, phone, email);

            double initialBalance = 123.45;
            IBankAccount account1 = new BankAccount(1, initialBalance);

            customer.AddBankAccount(account1);
            Assert.AreEqual(1, customer.BankAccounts.Count);
            Assert.AreSame(account1, customer.BankAccounts[0]);

            try
            {
                customer.RemoveBankAccount(account1);
                Assert.Fail("Removed non-empty bank account from customer");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, customer.BankAccounts.Count);
                Assert.AreEqual(initialBalance, customer.BankAccounts[0].Balance);
            }
        }

    }
}
