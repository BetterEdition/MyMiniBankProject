using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using BE;

namespace UnitTests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void CreateTransaction()
        {
            int id = 1;
            String message = "Some Message";
            double amount = 123.45;

            DateTime before = DateTime.Now;
            ITransaction t = new Transaction(id, message, amount);
            DateTime after = DateTime.Now;

            Assert.AreEqual(id, t.Id);
            Assert.IsTrue(before <= t.DateTime);
            Assert.IsTrue(after >= t.DateTime);
            Assert.AreEqual(message, t.Message);
            Assert.AreEqual(amount, t.Amount);
        }
    }
}
