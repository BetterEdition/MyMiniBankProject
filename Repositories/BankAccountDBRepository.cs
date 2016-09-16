using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    class BankAccountDBRepository : IRepository<IBankAccount, int>
    {
        private List<IBankAccount> accounts = new List<IBankAccount>();

        public int Count
        {
            get
            {
                return accounts.Count;
            }
        }

        public void Add(IBankAccount e)
        {
            
            throw new NotImplementedException();
        }

        public IList<IBankAccount> GetAll()
        {
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
