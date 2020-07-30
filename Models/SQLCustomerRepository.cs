using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Bank_Management.Models
{
    public class SQLCustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;

        public SQLCustomerRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Customer Add(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer;
        }

        public Customer Delete(int CustomerID)
        {
            Customer customer = context.Customers.Find(CustomerID);
            if(customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
            return customer;
        }

        public Customer GetCustomer(int CustomerID)
        {
            return context.Customers.Find(CustomerID);
        }

        public Customer Update(Customer customerChanges)
        {
            var customer = context.Customers.Attach(customerChanges);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return customerChanges;
        }

        public Customer GetCustomerBySSNID(int SSNID)
        {
            return context.Customers.FirstOrDefault(x=>x.SSNID == SSNID);
        }
    }
}
