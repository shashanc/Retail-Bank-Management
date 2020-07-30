using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Retail_Bank_Management.Models
{
    public class SQLAccountRepository : IAccountRepository
    {
        private readonly AppDbContext context;

        public SQLAccountRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Account Add(Account account)
        {
            context.Accounts.Add(account);
            context.SaveChanges();
            return account;
        }

        public Account GetAccount(int AccountID)
        {
            return context.Accounts.Find(AccountID);
        }

        public Account Delete(int AccountID)
        {
            
            Account account = context.Accounts.Find(AccountID);
            if(account != null)
            {
                context.Accounts.Remove(account);
                context.SaveChanges();
            }
            return account;
        }

        public Account GetAccountByCustomerID(int CustomerID)
        {
            return context.Accounts.FirstOrDefault(x=>x.CustomerID == CustomerID);
        }

        public List<Account> GetAllAccountsByCustomerID(int CustomerID)
        {
            return context.Accounts.Where(x => x.CustomerID == CustomerID).ToList();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return context.Accounts.ToList();
        }

        public Account Deposit(int AccountID, int Amount)
        {
            Account changedAcc = GetAccount(AccountID);
            if(changedAcc != null)
            {
                changedAcc.Balance += Amount;
                changedAcc.LastUpdated = DateTime.Now;
                context.Accounts.Update(changedAcc);
                context.SaveChanges();
                AddTransaction(AccountID, Amount, "Deposit");
            }
            return changedAcc;
        }

        public Account Withdraw(int AccountID, int Amount, out string success)
        {
            Account changedAcc = GetAccount(AccountID);
            if (changedAcc != null)
            {
                if (changedAcc.Balance >= Amount)
                {
                    
                    changedAcc.Balance -= Amount;
                    changedAcc.LastUpdated = DateTime.Now;
                    context.Accounts.Update(changedAcc);
                    context.SaveChanges();
                    success = "true";
                    AddTransaction(AccountID, Amount, "Withdraw");
                }
                else
                    success = "false";
            } else
            success = "not found";
            return changedAcc;
        }

        public TransferViewModel Transfer(TransferViewModel model)
        {
            
            Account fromAccount = GetAccount(model.FromAccount);
            Account toAccount = GetAccount(model.ToAccount);
            fromAccount.LastUpdated = DateTime.Now;
            toAccount.LastUpdated = DateTime.Now;
            fromAccount.Balance -= model.Amount;
            toAccount.Balance += model.Amount;
            model.SenderBalance = (int) fromAccount.Balance - model.Amount;
            model.BeneficiaryBalance = (int) toAccount.Balance + model.Amount;
            context.Accounts.Update(fromAccount);
            context.Accounts.Update(toAccount);
            context.SaveChanges();
            AddTransaction(model.FromAccount, model.Amount, "Transfer");
            AddTransaction(model.ToAccount, model.Amount, "Recieved");
            return model;
        }

        public Transaction AddTransaction(int id, int amnt, string desc)
        {
            Transaction trans = new Transaction
            {
                AccountID = id,
                Description = desc,
                Amount = amnt,
                TransactionDate = DateTime.Now,
            };
            context.Transactions.Add(trans);
            context.SaveChanges();
            return context.Transactions.FirstOrDefault(x=>x.AccountID == id);
        }

        public IEnumerable<Transaction> GetTransactionsByDate(AccountStatementViewModel model)
        {
            IEnumerable<Transaction> transactions = context.Transactions
                .Where(x => (x.TransactionDate >= model.StartDate && x.TransactionDate <= model.EndDate.AddDays(1) && x.AccountID == model.AccountID))
                .ToList();
            return transactions;
        }

        public IEnumerable<Transaction> GetTransactionsByNumber(AccountStatementViewModel model)
        {
            return context.Transactions.Where(x => x.AccountID == model.AccountID).Take(model.NumberOfTransactions).ToList();
        }
    }
}
