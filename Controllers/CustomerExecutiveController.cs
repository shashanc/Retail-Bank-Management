using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Retail_Bank_Management.Models;

namespace Retail_Bank_Management.Controllers
{
    [Authorize(Roles = "AccountExecutive")]
    public class CustomerExecutiveController : Controller
    {
        private ICustomerRepository _customerRepository;
        private IAccountRepository _accountRepository;

        public CustomerExecutiveController(ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Update(string SearchString)
        {
            Customer model = _customerRepository.GetCustomer(Convert.ToInt32(SearchString));
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Customer customerChanges)
        {
            if (ModelState.IsValid)
            {
                ViewData["success"] = "true";
                Customer newCustomer = _customerRepository.Update(customerChanges);
                return View(newCustomer);
                
            }
            ViewData["success"] = "false";
            return View(customerChanges);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if(ModelState.IsValid)
            {
                Customer newCustomer = _customerRepository.Add(customer);
                return View(newCustomer);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(string SearchString)
        {
            Customer model = _customerRepository.GetCustomer(Convert.ToInt32(SearchString));
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            ViewData["success"] = "true";
            Customer model = _customerRepository.Delete(customer.CustomerID);
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(Account account)
        {
            Account newAccount = new Account() 
            { CustomerID = account.CustomerID, Balance = account.Balance,
              AccountType = account.AccountType, Message = "Successfully Created", Status = "Active", LastUpdated = DateTime.Now };
            string customerName = _customerRepository.GetCustomer(account.CustomerID).Name;
            ViewData["customerName"] = customerName;
            Account model = _accountRepository.Add(newAccount);
            return View(model);
        }

        [HttpGet]
        public IActionResult GetAccount(string CustomerID)
        {
            DeleteAccountViewModel deleteAccountViewModel = null;
            Customer cust = _customerRepository.GetCustomer(Convert.ToInt32(CustomerID));
            Account acc = _accountRepository.GetAccountByCustomerID(Convert.ToInt32(CustomerID));
            if(cust != null && acc != null)
            {
                string name = cust.Name;
                int accountID = acc.AccountID;
                deleteAccountViewModel = new DeleteAccountViewModel() { CustomerName = name, AccountID = accountID };
            }
            else
            {
                ViewData["DeleteSuccess"] = "false";
                deleteAccountViewModel = new DeleteAccountViewModel() { CustomerName = "No Account", AccountID = 000000000 };
            }
            return View("DeleteAccount", deleteAccountViewModel);
        }

        [HttpGet]
        public IActionResult DeleteAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteAccount(DeleteAccountViewModel model)
        {
            Account account = _accountRepository.Delete(model.AccountID);
            if (account != null)
                ViewData["DeleteSuccess"] = "true";
            else 
                ViewData["DeleteSuccess"] = "false";
            return View(model);
            //return model.AccountID;
        }

        [HttpGet]
        public IActionResult AccountStatus()
        {
            List<Account> accounts = _accountRepository.GetAllAccounts().ToList();
            return View(accounts);
        }
    }
}