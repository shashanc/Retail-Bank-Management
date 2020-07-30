using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Retail_Bank_Management.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
             new Customer()
             {
                 SSNID = 123456789,
                 CustomerID = 100000000,
                 Address = "S/S MQ 133",
                 Address2 = "Dhori Staff Quarters",
                 Age = 23,
                 City = "Bokaro",
                 Name = "Shashank Sharma",
                 State = "Jharkhand"
             },

                new Customer()
                {
                    SSNID = 100456789,
                    CustomerID = 100000001,
                    Address = "MQ 123",
                    Address2 = "Kargali Staff Quarters",
                    Age = 21,
                    City = "Bokaro",
                    Name = "Nikhil Raj",
                    State = "Jharkhand"
                },

                new Customer()
                {
                    SSNID = 123459789,
                    CustomerID = 100000002,
                    Address = "S/S SQ 24",
                    Address2 = "Dhori Staff Quarters",
                    Age = 21,
                    City = "Bokaro",
                    Name = "Adarsh Singh",
                    State = "Jharkhand"
                }
            );

            modelBuilder.Entity<Account>().HasData(new Account() { AccountID = 203206016, 
            AccountType="Savings", Balance=5000, CustomerID=100000000, LastUpdated = DateTime.Now,
                Message = "Account Created", Status = "Active"});

        }
    }
}
