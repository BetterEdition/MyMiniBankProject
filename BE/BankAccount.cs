using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankAccount : IBankAccount
    {
        public const double DEFAULT_INTEREST_RATE = 0.01;
        public const double INTEREST_RATE_LOWERBOUND = 0.00;
        public const double INTEREST_RATE_UPPERBOUND = 0.10;
        private double m_interestRate;

        private int nextTransactionId = 1;

        public int AccountNumber { get; }
        public double Balance { get; private set; }
        public double InterestRate
        {
            get
            {
                return m_interestRate;
            }
            set
            {
                if (value < INTEREST_RATE_LOWERBOUND || value > INTEREST_RATE_UPPERBOUND)
                {
                    throw new ArgumentException("Interest Rate outside valid range");
                }
                m_interestRate = value;
            }
        }
        public IList<ITransaction> Transactions { get; private set; }

        public BankAccount(int accNumber) : this(accNumber, 0.0) { }

        public BankAccount(int accNumber, double initialBalance)
        {
            AccountNumber = accNumber;
            Balance = initialBalance;
            InterestRate = DEFAULT_INTEREST_RATE;
            Transactions = new List<ITransaction>();
        }

        public void Deposit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Cannot deposit a negative amount");
            }
            Balance += amount;
            Transactions.Add(new Transaction(nextTransactionId++, "Deposit", amount));
        }

        public void Withdraw(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Cannot withdraw a negative amount");
            }
            if (amount > Balance)
            {
                throw new ArgumentException("Amount to withdraw exceeds the balance");
            }
            Balance -= amount;
            Transactions.Add(new Transaction(nextTransactionId++, "Withdraw", -amount));
        }

        public void AddInterest()
        {
            double interest = Balance * InterestRate;
            Balance += interest;
            Transactions.Add(new Transaction(nextTransactionId++, "Interest", interest));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            IBankAccount other = (IBankAccount)obj;
            return (this.AccountNumber == other.AccountNumber);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return AccountNumber;
        }
    }
}
