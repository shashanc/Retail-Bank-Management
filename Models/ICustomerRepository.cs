using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Bank_Management.Models
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(int CustomerID);
        Customer Add(Customer customer);
        Customer Update(Customer customerChanges);
        Customer Delete(int CustomerID);
        Customer GetCustomerBySSNID(int SSNID);
    }
}
