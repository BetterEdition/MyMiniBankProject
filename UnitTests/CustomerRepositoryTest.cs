using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using Repositories;
using BE;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        [TestMethod]
        public void Create_CustomerRepository_Test()
        {
            IRepository<ICustomer, int> customers = new CustomerRepository();
            Assert.IsNotNull(customers);
            Assert.AreEqual(0, customers.Count);
        }

        [TestMethod]
        public void Add_New_Customer_Test()
        {
            IRepository<ICustomer, int> customers = new CustomerRepository();

            int custId = 1;
            ICustomer customer = new Customer(custId, "Name", "Address", "Phone", "Email");
    
            customers.Add(customer);

            Assert.AreEqual(1, customers.Count);
            Assert.AreSame(customer, customers.GetById(custId));
        }

        [TestMethod]
        public void Add_Existing_Customer_Expect_ArgumentException_Test()
        {
            IRepository<ICustomer, int> customers = new CustomerRepository();
            int custId = 1;
            ICustomer customer = new Customer(custId, "Name", "Address", "Phone", "Email");

            customers.Add(customer);

            try
            {
                customers.Add(customer);
                Assert.Fail("Added existing customer to repository");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, customers.Count);
                Assert.AreSame(customer, customers.GetById(custId));
            }
        }

        [TestMethod]
        public void Remove_Existing_Customer_Test()
        {
            IRepository<ICustomer, int> customers = new CustomerRepository();
            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(2, "Name", "Address", "Phone", "Email");
 
            customers.Add(customer1);
            customers.Add(customer2);

            customers.Remove(customer1);
            Assert.AreEqual(1, customers.Count);
            Assert.AreSame(customer2, customers.GetAll()[0]);
        }

        [TestMethod]
        public void Remove_NonExisting_Customer_Expect_ArgumentException_Test()
        {
            IRepository<ICustomer, int> customers = new CustomerRepository();
            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(2, "Name", "Address", "Phone", "Email");

            customers.Add(customer1);

            try
            {
                customers.Remove(customer2);
                Assert.Fail("Removed non-existing customer");
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, customers.Count);
                Assert.AreSame(customer1, customers.GetAll()[0]);
            }
        }

        [TestMethod]
        public void GetById_Existing_Customer_Test()
        {
            IRepository<ICustomer, int> customers = new CustomerRepository();
            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(2, "Name", "Address", "Phone", "Email");



            customers.Add(customer1);
            customers.Add(customer2);

            ICustomer returned = customers.GetById(2);
            Assert.IsNotNull(returned);
            Assert.AreSame(customer2, returned);
        }

        [TestMethod]
        public void GetById_NonExisting_Customer_ExpectNull_Test()
        {
            IRepository<ICustomer, int> accounts = new CustomerRepository();
            ICustomer customer = new Customer(1, "Name", "Address", "Phone", "Email");

            accounts.Add(customer);

            ICustomer returned = accounts.GetById(2);

            Assert.IsNull(returned);
        }

        [TestMethod]
        public void GetAll_Test()
        {
            IRepository<ICustomer, int> customers = new CustomerRepository();
            ICustomer customer1 = new Customer(1, "Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(2, "Name", "Address", "Phone", "Email");

            customers.Add(customer1);
            customers.Add(customer2);

            IList<ICustomer> returned = customers.GetAll();

            Assert.AreEqual(2, returned.Count);
            Assert.AreSame(customer1, returned[0]);
            Assert.AreSame(customer2, returned[1]);
        }
    }
}
