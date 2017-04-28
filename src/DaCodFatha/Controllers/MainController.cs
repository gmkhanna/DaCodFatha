using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DaCodFatha.Models;
using Microsoft.AspNetCore.Identity;
using DaCodFatha.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DaCodFatha.Controllers
{
    public class MainController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public MainController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
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

        // Get - NON-Authenticated Product Page
        public IActionResult Product()
        {
            return View();
        }

        // Get - AUTHENTICATED ADMIN Product Page
        public IActionResult ProdAdmin()
        {
            return View();
        }

        // Get - NON - Authenticated Newsletter Page
        public IActionResult Newsletter()
        {
            return View();
        }

        //Get - AUTHENTICATED Newsletter for ADMIN Page
        public IActionResult NewsAdmin()
        {
            return View();
        }

        // Post - LogOff
        [HttpPost]
        public async Task<IActionResult> LogoOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
