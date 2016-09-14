using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BE;
using Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace UnitTests
{
    [TestClass]
    public class BankAccountTest
    {
        [TestMethod]
        public void CreateBankAccount_With_Zero_Balance_Test()
        {
            int accNumber = 1;
            ICustomer owner = new Customer(1, "Some Name", "Some Address", "Some Phone", "Some Email");

            BankAccount acc = BankAccount.CreateBankAccount(owner, accNumber);

            Assert.IsNotNull(acc);
            Assert.AreEqual(accNumber, acc.AccountNumber);
            Assert.AreEqual(0.0, acc.Balance);
            Assert.AreEqual(BankAccount.DEFAULT_INTEREST_RATE, acc.InterestRate);

            Assert.IsNotNull(acc.Transactions);
            Assert.AreEqual(0, acc.Transactions.Count);

            Assert.AreEqual(1, acc.Owners.Count);
            Assert.AreSame(owner, acc.Owners[0]);

            Assert.AreEqual(1, owner.BankAccounts.Count);
            Assert.AreSame(acc, owner.BankAccounts[0]);
        }

        [TestMethod]
        public void CreateBankAccountWithInitialBalance()
        {
            int accNumber = 1;
            double initialBalance = 123.45;

            ICustomer owner = new Customer(1, "Some Name", "Some Address", "Some Phone", "Some Email");

            BankAccount acc = BankAccount.CreateBankAccount(owner, accNumber, initialBalance);

            Assert.IsNotNull(acc);
            Assert.AreEqual(accNumber, acc.AccountNumber);
            Assert.AreEqual(initialBalance, acc.Balance);
            Assert.AreEqual(BankAccount.DEFAULT_INTEREST_RATE, acc.InterestRate);

            Assert.IsNotNull(acc.Transactions);
            Assert.AreEqual(0, acc.Transactions.Count);

            Assert.AreEqual(1, acc.Owners.Count);
            Assert.AreSame(owner, acc.Owners[0]);

            Assert.AreEqual(1, owner.BankAccounts.Count);
            Assert.AreSame(acc, owner.BankAccounts[0]);
        }

        [TestMethod]
        public void DepositPositiveAmount()
        {
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber, initialBalance);

            double amount = 23.45;

            DateTime before = DateTime.Now;

            account.Deposit(amount);

            Assert.AreEqual(initialBalance + amount, account.Balance);
            Assert.AreEqual(1, account.Transactions.Count);
            ITransaction t = account.Transactions[0];
            Assert.AreEqual(1, t.Id);
            Assert.IsTrue(before <= t.DateTime);
            Assert.IsTrue(DateTime.Now >= t.DateTime);
            Assert.AreEqual("Deposit", t.Message);
            Assert.AreEqual(amount, t.Amount);
        }

        [TestMethod]
        // [ExpectedException(typeof(ArgumentException))]
        public void DepositNegativeAmountExpectArgumentException()
        {
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber, initialBalance);

            double amount = -23.45;
            try
            {
                account.Deposit(amount);
                Assert.Fail("Deposit of a negative amount");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(initialBalance, account.Balance);
                Assert.AreEqual(0, account.Transactions.Count);
            }
        }

        [TestMethod]
        public void WithdrawPositiveAmountNotExceedingBalance()
        {
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber, initialBalance);

            double amount = 23.45;

            DateTime before = DateTime.Now;
            account.Withdraw(amount);
            DateTime after = DateTime.Now;

            Assert.AreEqual(initialBalance - amount, account.Balance);
            Assert.AreEqual(1, account.Transactions.Count);
            ITransaction t = account.Transactions[0];
            Assert.AreEqual(1, t.Id);
            Assert.IsTrue(before <= t.DateTime);
            Assert.IsTrue(after >= t.DateTime);
            Assert.AreEqual("Withdraw", t.Message);
            Assert.AreEqual(-amount, t.Amount);
        }

        [TestMethod]
        public void WithdrawNegativeAmountExpectArgumentException()
        {
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber, initialBalance);

            double amount = -23.45;

            try
            {
                account.Withdraw(amount);
                Assert.Fail("Withdraw of a negative amount");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(initialBalance, account.Balance);
                Assert.AreEqual(0, account.Transactions.Count);
            }

        }

        [TestMethod]
        public void WithdrawPositiveAmountExceedingBalanceExpectArgumentExpecption()
        {
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber, initialBalance);

            double amount = 123.46;

            try
            {
                account.Withdraw(amount);
                Assert.Fail("Withdraw of amount exceeding the balance");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(initialBalance, account.Balance);
                Assert.AreEqual(0, account.Transactions.Count);
            }
        }

        [TestMethod]
        public void AddInterestToBalance()
        {
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber, initialBalance);

            double interest = initialBalance * account.InterestRate;

            DateTime before = DateTime.Now;
            account.AddInterest();
            DateTime after = DateTime.Now;

            Assert.AreEqual(initialBalance + interest, account.Balance);
            Assert.AreEqual(1, account.Transactions.Count);
            ITransaction t = account.Transactions[0];
            Assert.AreEqual(1, t.Id);
            Assert.IsTrue(before <= t.DateTime);
            Assert.IsTrue(after >= t.DateTime);
            Assert.AreEqual("Interest", t.Message);
            Assert.AreEqual(interest, t.Amount);
        }

        [TestMethod]
        public void ChangeInterestRateInsideRange()
        {
            double interestRateLowerBound = BankAccount.INTEREST_RATE_LOWERBOUND;
            double interestRateUpperBound = BankAccount.INTEREST_RATE_UPPERBOUND;

            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber);

            account.InterestRate = interestRateLowerBound;
            Assert.AreEqual(interestRateLowerBound, account.InterestRate);

            account.InterestRate = interestRateUpperBound;
            Assert.AreEqual(interestRateUpperBound, account.InterestRate);
        }

        [TestMethod]
        public void ChangeInterestRateOutsideRange()
        {
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber);

            double oldInterestRate = account.InterestRate;

            try
            {
                account.InterestRate = BankAccount.INTEREST_RATE_LOWERBOUND - 0.01;
                Assert.Fail("Interest Rate below lowerbound");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(oldInterestRate, account.InterestRate);
            }

            try
            {
                account.InterestRate = BankAccount.INTEREST_RATE_UPPERBOUND + 0.01;
                Assert.Fail("Interest Rate above upperbound");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(oldInterestRate, account.InterestRate);
            }
        }

        [TestMethod]
        public void GetTransactionListBetweenValidDates()
        {
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber);

            account.Deposit(1000);
            Thread.Sleep(1000);
            DateTime from = DateTime.Now;
            account.Withdraw(400);
            Thread.Sleep(1000);
            DateTime to = DateTime.Now;
            Thread.Sleep(1000);
            account.Deposit(200);

            Assert.AreEqual(3, account.Transactions.Count);
            IList<ITransaction> result = account.GetTransactions(from, to);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreSame(result[0], account.Transactions[1]);
        }

        [TestMethod]
        public void GetTransactionListFromValidDate()
        {
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            int accNumber = 1;
            IBankAccount account = BankAccount.CreateBankAccount(customer, accNumber);

            account.Deposit(1000);
            Thread.Sleep(1000);
            DateTime from = DateTime.Now;
            account.Withdraw(400);
            Thread.Sleep(1000);
            account.Deposit(200);

            Assert.AreEqual(3, account.Transactions.Count);
            IList<ITransaction> result = account.GetTransactions(from);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreSame(result[0], account.Transactions[1]);
            Assert.AreSame(result[1], account.Transactions[2]);
        }
    }
}
