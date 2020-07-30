using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Retail_Bank_Management.Models;

namespace Retail_Bank_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
                              RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                

                var role = roleManager.Roles.FirstOrDefault(x=>x.Name == model.Role);
                if(role == null)
                {
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = model.Role
                    };
                    IdentityResult roleResult = await roleManager.CreateAsync(identityRole);
                }

                var user1 = await userManager.FindByNameAsync(model.Email);
                IdentityResult addRoleResult = await userManager.AddToRoleAsync(user1, model.Role);
                
                if(result.Succeeded && addRoleResult.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "CashierTeller");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                foreach (var error in addRoleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("AccountExecutive"))
                {
                    return RedirectToAction("Index","CustomerExecutive");
                } else
                {
                    return RedirectToAction("Index","CashierTeller");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(model.Email);
                    if(await userManager.IsInRoleAsync(user,"AccountExecutive"))
                    {
                        return RedirectToAction("Index", "CustomerExecutive");
                    }
                    else
                    {
                        return RedirectToAction("Index", "CashierTeller");
                    }
                }
                
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
