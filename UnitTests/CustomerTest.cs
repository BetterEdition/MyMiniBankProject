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
            Assert.AreEqual(addresss, customer.Address);
            Assert.AreEqual(phone, customer.Phone);
            Assert.AreEqual(email, customer.Email);
            Assert.IsNotNull(customer.BankAccounts);
            Assert.AreEqual(0, customer.BankAccounts.Count);
        }

        [TestMethod]
        public void AddNewBankAccountToCustomer()
        {
            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2= new Customer(2, "Name", "Address", "Phone", "Email");

            IBankAccount account1 = BankAccount.CreateBankAccount(customer1, 1);
            IBankAccount account2 = BankAccount.CreateBankAccount(customer2, 2);

            customer1.AddBankAccount(account2);
            Assert.AreEqual(2, customer1.BankAccounts.Count);
            Assert.AreSame(account1, customer1.BankAccounts[0]);
            Assert.AreSame(account2, customer1.BankAccounts[1]);
        }


        [TestMethod]
        public void AddExistingBankAccountToCustomerExpectArgumentException()
        {
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber, initialBalance);

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
            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(2, "Name", "Address", "Phone", "Email");

            IBankAccount account1 = BankAccount.CreateBankAccount(customer1, 1);
            IBankAccount account2 = BankAccount.CreateBankAccount(customer2, 2);


            customer1.AddBankAccount(account2);
            Assert.AreEqual(2, customer1.BankAccounts.Count);
            Assert.AreSame(account1, customer1.BankAccounts[0]);
            Assert.AreSame(account2, customer1.BankAccounts[1]);

            customer1.RemoveBankAccount(account1);

            Assert.AreEqual(1, customer1.BankAccounts.Count);
            Assert.AreSame(account2, customer1.BankAccounts[0]);
        }

        [TestMethod]

        public void RemoveNonExistingBankAccountFromCustomerExpectArgumentException()
        {
            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(2, "Name", "Address", "Phone", "Email");

            IBankAccount account1 = BankAccount.CreateBankAccount(customer1, 1);
            IBankAccount account2 = BankAccount.CreateBankAccount(customer2, 2);

            Assert.AreEqual(1, customer1.BankAccounts.Count);
            Assert.AreSame(account1, customer1.BankAccounts[0]);

            try
            {
                customer1.RemoveBankAccount(account2);
                Assert.Fail("Removed non-existing bank account from customer");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, customer1.BankAccounts.Count);
                Assert.AreSame(account1, customer1.BankAccounts[0]);
            }
        }

        [TestMethod]
        public void RemoveNonEmptyBankAccountFromCustomerExpectArgumentException()
        {
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account1 = BankAccount.CreateBankAccount(customer, accNumber, initialBalance);

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
