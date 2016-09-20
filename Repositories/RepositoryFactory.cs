using Interfaces;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryFactory<T, K>
    {
        public static IRepository<T, K> CreateNewRepository()
        {
            return new Repository<T, K>();
        }
    }
}
