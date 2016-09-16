using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using BLL;
using Repositories;
using System.Collections.Generic;
using BE;

namespace UnitTests
{
    [TestClass]
    public class CustomerControllerTest
    {
        [TestMethod]
        public void CreateNewCustomer_Test()
        {
            IRepository<ICustomer, int> repository = new CustomerRepository();
            ICustomerController mgr = new CustomerController(repository);
            String name = "Name";
            String address = "Address";
            String phone = "Phone";
            String email = "Email";

            ICustomer c = mgr.CreateNewCustomer(name, address, phone, email);

            Assert.IsNotNull(c);
            Assert.AreEqual(1, c.Id);
            Assert.AreEqual(name, c.Name);
            Assert.AreEqual(address, c.Address);
            Assert.AreEqual(phone, c.Phone);
            Assert.AreEqual(email, c.Email);
            Assert.AreEqual(0, c.BankAccounts.Count);
        }

        [TestMethod]
        public void CreateNewCustomer_Name_Or_Adress_is_Null_Expect_ArgumentNullException_Test()
        {
            IRepository<ICustomer, int> repository = new CustomerRepository();
            ICustomerController mgr = new CustomerController(repository);

            ICustomer customer = null;
            try
            {
                mgr.CreateNewCustomer(null, "Adress", "Phone", "Email");
                Assert.Fail("Accepted null value for Name");
            }
            catch (ArgumentNullException)
            {
                Assert.IsNull(customer);
            }

            try
            {
                mgr.CreateNewCustomer("Name", null, "Phone", "Email");
                Assert.Fail("Accepted null value for Address");
            }
            catch (ArgumentNullException)
            {
                Assert.IsNull(customer);
            }
        }
        [TestMethod]
        public void CreateNewCustomer_Name_Or_Adress_is_Empty_Expect_ArgumentException_Test()
        {
            IRepository<ICustomer, int> repository = new CustomerRepository();
            ICustomerController mgr = new CustomerController(repository);

            ICustomer customer = null;
            try
            {
                mgr.CreateNewCustomer("", "Adress", "Phone", "Email");
                Assert.Fail("Accepted empty Name");
            }
            catch (ArgumentException)
            {
                Assert.IsNull(customer);
            }

            try
            {
                mgr.CreateNewCustomer("Name", "", "Phone", "Email");
                Assert.Fail("Accepted empty Address");
            }
            catch (ArgumentException)
            {
                Assert.IsNull(customer);
            }
        }

        [TestMethod]
        public void GetAllCustomers_Test()
        {
            IRepository<ICustomer, int> repository = new CustomerRepository();
            ICustomerController mgr = new CustomerController(repository);

            ICustomer customer1 = mgr.CreateNewCustomer("Name", "Address", "Phone", "Email");
            ICustomer customer2 = mgr.CreateNewCustomer("Name", "Address", "Phone", "Email");

            IList<ICustomer> result = mgr.GetAllCustomers();
            Assert.AreEqual(2, result.Count);
            Assert.AreSame(customer1, result[0]);
            Assert.AreSame(customer2, result[1]);
        }

        [TestMethod]
        public void GetCustomerById_Existing_Customer_Test()
        {
            IRepository<ICustomer, int> repository = new CustomerRepository();
            ICustomerController mgr = new CustomerController(repository);

            ICustomer customer1 = mgr.CreateNewCustomer("Name", "Address", "Phone", "Email");
            ICustomer customer2 = mgr.CreateNewCustomer("Name", "Address", "Phone", "Email");

            ICustomer result = mgr.GetCustomerById(customer2.Id);
            Assert.IsNotNull(result);
            Assert.AreSame(customer2, result);
        }

        [TestMethod]
        public void RemoveCustomer_Existing_Customer_With_No_BankAccount_Test()
        {
            IRepository<ICustomer, int> repository = new CustomerRepository();
            ICustomerController mgr = new CustomerController(repository);

            ICustomer customer = mgr.CreateNewCustomer("Name", "Address", "Phone", "Email");

            Assert.AreEqual(1, mgr.GetAllCustomers().Count);

            mgr.RemoveCustomer(customer);

            Assert.AreEqual(0, mgr.GetAllCustomers().Count);
        }

        [TestMethod]
        public void RemoveCustomer_Non_Existing_Customer_Expect_ArgumentException_Test()
        {
            IRepository<ICustomer, int> repository = new CustomerRepository();
            ICustomerController mgr = new CustomerController(repository);

            ICustomer customer1 = mgr.CreateNewCustomer("Name", "Address", "Phone", "Email");
            ICustomer customer2 = new Customer(100, "Name", "Address", "Phone", "Email");

            Assert.AreEqual(1, mgr.GetAllCustomers().Count);

            try
            {
                mgr.RemoveCustomer(customer2);
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, mgr.GetAllCustomers().Count);
                Assert.AreSame(customer1, mgr.GetAllCustomers()[0]);
            }
        }

        [TestMethod]
        public void RemoveCustomer_Existing_Customer_With_NonEmpty_BankAccount_Test()
        {
            IRepository<ICustomer, int> repository = new CustomerRepository();
            ICustomerController mgr = new CustomerController(repository);

            ICustomer customer1 = mgr.CreateNewCustomer("Name", "Address", "Phone", "Email");
            BankAccountController accMgr = new BankAccountController(new BankAccountRepository());
            accMgr.CreateNewBankAccount(customer1, 123.45);

            Assert.AreEqual(1, mgr.GetAllCustomers().Count);
            Assert.AreSame(customer1, mgr.GetAllCustomers()[0]);
            Assert.AreEqual(1, customer1.BankAccounts.Count);
            Assert.AreEqual(123.45, customer1.BankAccounts[0].Balance);

            try
            {
                mgr.RemoveCustomer(customer1);
            }
            catch (ArgumentException)
            {
                Assert.AreEqual(1, mgr.GetAllCustomers().Count);
                Assert.AreSame(customer1, mgr.GetAllCustomers()[0]);
            }
        }
    }
}
