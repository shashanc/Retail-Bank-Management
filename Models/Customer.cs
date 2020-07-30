using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Bank_Management.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "SSN is Required")]
        [RegularExpression(@"^\d{9}|\d{3}-\d{2}-\d{4}$", ErrorMessage = "Invalid Social Security Number")]
        public int SSNID { get; set; }
        public int CustomerID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name exceeded 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Incorrect Name Format")]
        public string Name { get; set; }
        [Required]
        [Range(1, 120, ErrorMessage = "Invalid Age")]
        public byte Age { get; set; }
        [Required]
        public string Address { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
    }
}
