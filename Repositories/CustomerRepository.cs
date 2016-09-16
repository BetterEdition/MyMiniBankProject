using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{

    public class CustomerRepository : IRepository<ICustomer, int>
    {
        private IList<ICustomer> customers = new List<ICustomer>();

        public int Count
        {
            get
            {
                return customers.Count;
            }
        }

        public void Add(ICustomer e)
        {
            if (customers.Contains(e))
            {
                throw new ArgumentException("Customer already exist.");
            }
            customers.Add(e);
        }

        public IList<ICustomer> GetAll()
        {
            return customers.ToList();
        }

        public ICustomer GetById(int id)
        {
            return customers.Where(c => c.Id == id).SingleOrDefault();
        }

        public void Remove(ICustomer e)
        {
            if (!customers.Remove(e))
            {
                throw new ArgumentException("Customer does not exist");
            }
        }
    }
}
