using BE;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BankAccountDBRepository : IRepository<IBankAccount, int>
    {
        private const String connectionString = @"Data Source = BHP-Q215-LT\SQLEXPRESS; Initial Catalog = MiniBankDB; Integrated Security = True; Pooling = True";
        private List<IBankAccount> accounts = new List<IBankAccount>();

        public int Count
        {
            get
            {
                return -1;
            }
        }

        public void Add(IBankAccount e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Insert into BankAccount values (@accNumber, @balance, @interestRate)";
                cmd.Parameters.AddWithValue("accNumber", e.AccountNumber);
                cmd.Parameters.AddWithValue("balance", e.Balance);
                cmd.Parameters.AddWithValue("interestRate", e.InterestRate);
                int inserted = cmd.ExecuteNonQuery();
                if (inserted == 1)
                    accounts.Add(e);

            }
        }

        public IList<IBankAccount> GetAll()
        {
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand cmd = connection.CreateCommand();
            //    cmd.CommandText = "SELECT * FROM BankAccount";
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        int accNumber = reader.GetInt32(0);
            //        double balance = reader.GetDouble(1);
            //        double interestRate = reader.GetDouble(2);
            //        IBankAccount account = new BankAccount(accNumber, balance, interestRate);
            //    }
            //}
            throw new NotImplementedException();
        }

        public IBankAccount GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(IBankAccount e)
        {
            throw new NotImplementedException();
        }
    }
}
