using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Bank_Management.Models
{
    public class MockCustomerRepository : ICustomerRepository
    {
        private List<Customer> _customerList;

        public MockCustomerRepository()
        {
            _customerList = new List<Customer>()
            {
                new Customer() { SSNID = 123456789, CustomerID = 121345676, 
                Address = "S/S MQ 133", Address2 = "Dhori Staff Quarters", 
                Age = 23, City = "Bokaro", Name = "Shashank Sharma", State = "Jharkhand" },

                new Customer() { SSNID = 100456789, CustomerID = 100345676,
                Address = "MQ 123", Address2 = "Kargali Staff Quarters",
                Age = 21, City = "Bokaro", Name = "Nikhil Raj", State = "Jharkhand" },

                new Customer() { SSNID = 123459789, CustomerID = 1233445676,
                Address = "S/S SQ 24", Address2 = "Dhori Staff Quarters",
                Age = 21, City = "Bokaro", Name = "Adarsh Singh", State = "Jharkhand" }
            };
        }

        public Customer Add(Customer customer)
        {
            customer.CustomerID = _customerList.Max(c => c.CustomerID) + 1;
            _customerList.Add(customer);
            return customer;
        }

        public Customer Delete(int CustomerID)
        {
            Customer customer = _customerList.FirstOrDefault(c => c.CustomerID == CustomerID);
            if(customer != null)
            {
                _customerList.Remove(customer);
            }
            return customer;
        }

        public Customer GetCustomer(int CustomerID)
        {
            return _customerList.FirstOrDefault(c => c.CustomerID == CustomerID);
        }

        public Customer GetCustomerBySSNID(int SSNID)
        {
            throw new NotImplementedException();
        }

        public Customer Update(Customer customerChanges)
        {
            Customer customer = _customerList.FirstOrDefault(c => c.CustomerID == customerChanges.CustomerID);
            if (customer != null)
            {
                customer.Name = customerChanges.Name;
                customer.Age = customerChanges.Age;
                customer.Address = customerChanges.Address;
            }
            return customer;
        }
    }
}
