using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Bank_Management.Models
{
    public class TransferViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [Range(100000000,999999999, ErrorMessage = "Invalid Account No.")]
        
        public int FromAccount { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(100000000, 999999999, ErrorMessage = "Invalid Account No.")]
        public int ToAccount { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(1,int.MaxValue, ErrorMessage = "Invalid Amount")]
        public int Amount { get; set; }
        public int SenderBalance { get; set; }
        public int BeneficiaryBalance { get; set; }
    }
}
