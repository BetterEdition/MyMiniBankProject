using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryFactory
    {
        public static IRepository<IBankAccount, int> CreateNewRepository()
        {
            return new BankAccountRepository();
        }
    }
}
