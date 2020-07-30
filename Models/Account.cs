using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Bank_Management.Models
{
    public class Account
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Account ID is required")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int AccountID { get; set; }
        public string AccountType { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime LastUpdated { get; set; }
        public long Balance { get; set; }
    }
}
