using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BE;
using Interfaces;

namespace UnitTests
{
    [TestClass]
    public class BankAccountTest
    {
        [TestMethod]
        public void CreateBankAccountWithZeroBalance()
        {
            int accNumber = 1;
            BankAccount acc = new BankAccount(accNumber);

            Assert.IsNotNull(acc);
            Assert.AreEqual(accNumber, acc.AccountNumber);
            Assert.AreEqual(0.0, acc.Balance);
            Assert.IsNotNull(acc.Transactions);
            Assert.AreEqual(0, acc.Transactions.Count);
            Assert.AreEqual(BankAccount.DEFAULT_INTEREST_RATE, acc.InterestRate);
        }
        [TestMethod]
        public void CreateBankAccountWithInitialBalance()
        {
            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(accNumber, initialBalance);

            Assert.IsNotNull(acc);
            Assert.AreEqual(accNumber, acc.AccountNumber);
            Assert.AreEqual(initialBalance, acc.Balance);
            Assert.IsNotNull(acc.Transactions);
            Assert.AreEqual(0, acc.Transactions.Count);
            Assert.AreEqual(BankAccount.DEFAULT_INTEREST_RATE, acc.InterestRate);
        }

        [TestMethod]
        public void DepositPositiveAmount()
        {
            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = new BankAccount(accNumber, initialBalance);
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
            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = new BankAccount(accNumber, initialBalance);
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
            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = new BankAccount(accNumber, initialBalance);
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
            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = new BankAccount(accNumber, initialBalance);
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
            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = new BankAccount(accNumber, initialBalance);
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
            int accNumber = 1;
            double initialBalance = 123.45;
            IBankAccount account = new BankAccount(accNumber, initialBalance);

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
            int accNumber = 1;
            double interestRateLowerBound = 0.0;
            double interestRateUpperBound = 0.10;
            IBankAccount account = new BankAccount(accNumber);

            account.InterestRate = interestRateLowerBound;
            Assert.AreEqual(interestRateLowerBound, account.InterestRate);

            account.InterestRate = interestRateUpperBound;
            Assert.AreEqual(interestRateUpperBound, account.InterestRate);
        }

        [TestMethod]
        public void ChangeInterestRateOutsideRange()
        {
            int accNumber = 1;
            IBankAccount account = new BankAccount(accNumber);

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
    }
}
