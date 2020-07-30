using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Bank_Management.Models
{
    public interface IAccountRepository
    {
        Account Add(Account account);

        Account GetAccount(int AccountID);

        Account GetAccountByCustomerID(int CustomerID);

        Account Delete(int AccountID);

        IEnumerable<Account> GetAllAccounts();

        List<Account> GetAllAccountsByCustomerID(int CustomerID);

        Account Deposit(int AccountID, int Amount);

        Account Withdraw(int AccountID, int Amount, out string success);

        TransferViewModel Transfer(TransferViewModel model);

        Transaction AddTransaction(int id, int amnt, string desc);

        IEnumerable<Transaction> GetTransactionsByDate(AccountStatementViewModel model);

        IEnumerable<Transaction> GetTransactionsByNumber(AccountStatementViewModel model);
    }
}
