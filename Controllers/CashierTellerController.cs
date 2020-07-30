using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Retail_Bank_Management.Models;

namespace Retail_Bank_Management.Controllers
{
    [Authorize(Roles = "CashierTeller")]
    public class CashierTellerController : Controller
    {
        private ICustomerRepository _customerRepository;
        private IAccountRepository _accountRepository;

        public CashierTellerController(ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccountDetails()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string SearchType, string SearchString)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(SearchString))
            {
                id = Convert.ToInt32(SearchString);
            }

            ViewData["SearchType"] = SearchType;
            if (SearchType == "CustomerID")
            {
                List<Account> accounts = _accountRepository.GetAllAccountsByCustomerID(id);
                return View("AccountDetails",accounts);
            } 
            else if(SearchType == "SSNID")
            {
                Customer customer = _customerRepository.GetCustomerBySSNID(id);
                if(customer != null)
                {
                    int customerID = _customerRepository.GetCustomerBySSNID(id).CustomerID;
                    List<Account> accounts = _accountRepository.GetAllAccountsByCustomerID(customerID);
                    return View("AccountDetails", accounts);
                }
                return View("AccountDetails", null);
            }
            else if(SearchType == "AccountID")
            {
                Account account = _accountRepository.GetAccount(id);
                ViewBag.Account = account;
                return View("AccountDetails",account);
            }
            return View("AccountDetails");
        }

        [HttpGet]
        public IActionResult Deposit(int id)
        {
            Account account = null;
            if(id != 0)
            {
                account = _accountRepository.GetAccount(id);
            }
            return View(account);
        }

        [HttpPost]
        public IActionResult Deposit(int AccountID, int Amount)
        {
            Account model = _accountRepository.Deposit(AccountID, Amount);
            if (model != null) ViewData["Success"] = "True";
            else ViewData["Success"] = "False";
            return View(model);
        }

        [HttpGet]
        public IActionResult Withdraw(int AccountID)
        {
            if (ModelState.IsValid)
            {
                Account account = null;
                if (AccountID != 0)
                {
                    account = _accountRepository.GetAccount(AccountID);
                }
                return View(account);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Withdraw(int AccountID, int Amount)
        {
            string success;
            Account model = _accountRepository.Withdraw(AccountID, Amount, out success);
            if (success == "true") ViewData["Success"] = "True";
            else if (success == "false") ViewData["Success"] = "False";
            else ViewData["Success"] = success;
            return View(model);
        }

        [HttpGet]
        public IActionResult Transfer(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                TransferViewModel transferViewModel = null;
                Account account = _accountRepository.GetAccount(model.FromAccount);
                if (account != null)
                {
                    transferViewModel = new TransferViewModel();
                    transferViewModel.FromAccount = account.AccountID;
                    ViewData["AccountFound"] = "true";
                }
                else ViewData["AccountFound"] = "false";
                return View(transferViewModel);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Transfer(TransferViewModel model, int i=0)
        {
            if (ModelState.IsValid)
            {
                Account toAccount = _accountRepository.GetAccount(model.ToAccount);
                if (toAccount != null)
                {
                   
                    ViewData["toAccountFound"] = "true";
                    
                    Account fromAccount = _accountRepository.GetAccount(model.FromAccount);
                    if (fromAccount.Balance >= model.Amount)
                    {
                        TransferViewModel transferViewModel = _accountRepository.Transfer(model);
                        ViewData["Transfer"] = "true";
                    }
                    else 
                        ViewData["Transfer"] = "false";
                }
                else 
                    ViewData["toAccountFound"] = "false";

                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AccountStatement()
        {
            IEnumerable<Account> accounts = _accountRepository.GetAllAccounts();
            ViewBag.Accounts = accounts;
            return View();
        }

        [HttpPost]
        public IActionResult AccountStatement(AccountStatementViewModel model)
        {
            List<Transaction> trans = null;
            if (model.ByDate)
            {
                trans = _accountRepository.GetTransactionsByDate(model).ToList();
            }
            else
            {
                trans = _accountRepository.GetTransactionsByNumber(model).ToList();
            }
            model.Transactions = trans;
            IEnumerable<Account> accounts = _accountRepository.GetAllAccounts();
            ViewBag.Accounts = accounts;
            return View(model);
        }
    }
}
