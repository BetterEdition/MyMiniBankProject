using BE;
using Interfaces;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class CustomerController : ICustomerController
    {
        private int nextCustomerNumber = 1;
        private IRepository<ICustomer, int> customers;

        public CustomerController(IRepository<ICustomer, int> repository)
        {
            customers = repository;
        }

        public void AddBankAccountToCustomer(ICustomer c, IBankAccount acc)
        {
            throw new NotImplementedException();
        }

        public ICustomer CreateNewCustomer(string name, string address, string phone, string email)
        {
            if (name == null || address == null)
            {
                throw new ArgumentNullException("Name or Address is missing");
            }
            name = name.Trim();
            address = address.Trim();
            if (name.Length == 0 || address.Length == 0)
            {
                throw new ArgumentException("Name or Address is empty");
            }
            phone = phone.Trim();
            email = email.Trim();

            ICustomer customer = new Customer(nextCustomerNumber++, name, address, phone, email);
            customers.Add(customer);
            return customer;           
        }

        public IList<ICustomer> GetAllCustomers()
        {
            return customers.GetAll();
        }

        public ICustomer GetCustomerById(int id)
        {
            return customers.GetById(id);
        }

        public void RemoveBankAccountFromCustomer(ICustomer c, IBankAccount acc)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomer(ICustomer customer)
        {
            customers.Remove(customer);
        }
    }
}
