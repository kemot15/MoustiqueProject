using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moustique.Models.Db;
using Moustique.Models.ViewModels;

namespace Moustique.Controllers
{
    public class AccountController : Controller
    {
        protected UserManager<User> UserManager { get; }
        protected SignInManager<User> SignInManager { get; }

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                //if (result.IsLockedOut)
                //{
                //    ModelState.AddModelError("", "Uzytkownik jest zablokowany");
                //   // return View(loginModel);
                //}

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Uzytkownik jest zablokowany");
                }
                else
                {
                    ModelState.AddModelError("", "Błąd logowania");
                }
            }
            return View(loginModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
