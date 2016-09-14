using Interfaces;
using System;

namespace BE
{
    public class Transaction : ITransaction
    {
        public int Id { get; }
        public DateTime DateTime { get; }
        public String Message { get; }
        public double Amount { get;}
        public Transaction(int id, string message, double amount)
        {
            Id = id;
            DateTime = DateTime.Now;
            Message = message;
            Amount = amount;
        }
    }
}