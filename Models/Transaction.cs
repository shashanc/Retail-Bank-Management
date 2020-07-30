using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Bank_Management.Models
{
    public class Transaction
    {
        
        public int AccountID { get; set; }

        [Key]
        public int TransactionID { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
    }
}
