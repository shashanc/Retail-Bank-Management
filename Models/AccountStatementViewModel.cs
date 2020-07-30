using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Bank_Management.Models
{
    public class AccountStatementViewModel
    {
        public int AccountID { get; set; }
        public int NumberOfTransactions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool ByDate { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
