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

        public IList<ICustomer> Owners { get; private set; }
        public IList<ITransaction> Transactions { get; private set; }

        public static BankAccount CreateBankAccount(ICustomer owner, int accNumber, double initialBalance = 0.00)
        {
            BankAccount account = new BankAccount(accNumber, initialBalance);
            account.AddOwner(owner);
            return account;
        }

        internal BankAccount(int accNumber, double initialBalance)
        {
            AccountNumber = accNumber;
            Balance = initialBalance;
            InterestRate = DEFAULT_INTEREST_RATE;
            Transactions = new List<ITransaction>();
            Owners = new List<ICustomer>();
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

        public IList<ITransaction> GetTransactions(DateTime from, DateTime to)
        {
            return Transactions.Where(t => t.DateTime >= from && t.DateTime <= to).ToList();           
        }

        public IList<ITransaction> GetTransactions(DateTime fromDate)
        {
            return (from t in Transactions where t.DateTime >= fromDate select t).ToList();
        }

        public void AddOwner(ICustomer owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("No owner specified");
            }
            if (Owners.Contains(owner))
            {
                throw new ArgumentException("Owner already registered for bank account.");
            }
            Owners.Add(owner);
            owner.AddBankAccount(this);
        }

        public void RemoveOwner(ICustomer owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("No owner specified");
            }
            if (!Owners.Contains(owner))
            {
                throw new ArgumentException("Owner not registered for bank account.");
            }
            owner.BankAccounts.Remove(this);
            Owners.Remove(owner);
        }
    }
}
