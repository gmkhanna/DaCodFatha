using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DaCodFatha.Models;
using DaCodFatha.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DaCodFatha.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //Get - Register Page
        public IActionResult Register()
        {
            return View();
        }

        // Post - Register -> to Register a new user and save this user to db
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // Get - Login Page
        public IActionResult Login()
        {
            return View();
        }

        //Post - Login info and route to index page after login
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // Post - LogOff
        public async Task<IActionResult> LogoOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
