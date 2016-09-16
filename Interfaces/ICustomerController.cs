using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICustomerController
    {
        ICustomer CreateNewCustomer(String name, String address, String phone, String email);
        void RemoveCustomer(ICustomer customer);
        IList<ICustomer> GetAllCustomers();
        ICustomer GetCustomerById(int id);
        void AddBankAccountToCustomer(ICustomer c, IBankAccount acc);
        void RemoveBankAccountFromCustomer(ICustomer c, IBankAccount acc);
    }
}
