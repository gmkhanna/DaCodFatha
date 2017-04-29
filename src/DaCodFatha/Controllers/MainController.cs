using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DaCodFatha.Models;
using Microsoft.AspNetCore.Identity;
using DaCodFatha.ViewModels;
using System.Security.Claims;

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
            return View(_db.Products.ToList());
        }

        // Get - AUTHENTICATED ADMIN Product Page
        public IActionResult ProdAdmin()
        {
            return View(_db.Products.ToList());
        }

        //Get - AUTHENTICATED ADMIN CREATE func
        public IActionResult Create()
        {
            return View();
        }

        //Post - AUTHENTICATED ADMIN CREATE func
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            product.User = currentUser;
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("ProdAdmin", "Main");
        }

        //Get - AUTHENTICATED ADMIN EDIT func
        public IActionResult EDIT(int id)
        {
            var thisProd = _db.Products.FirstOrDefault(p => p.Id == id);
            return View(thisProd);
        }

        //Post - AUTHENTICATED ADMIN EDIT func
        [HttpPost]
        public async Task<IActionResult> EDIT(Product product)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            product.User = currentUser;
            _db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("ProdAdmin", "Main");
        }

        //Get - AUTHENTICATED ADMIN DELETE func
        public IActionResult Delete(int id)
        {
            var thisProd = _db.Products.FirstOrDefault(p => p.Id == id);
            return View(thisProd);
        }

        //Post - AUTHENTICATED ADMIN DELETE func
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            //product.User = currentUser;
            var thisProd = _db.Products.FirstOrDefault(p => p.Id == id);
            _db.Products.Remove(thisProd);
            _db.SaveChanges();
            return RedirectToAction("ProdAdmin", "Main");
        }

        // Get - NON - Authenticated Newsletter Page
        public IActionResult Newsletter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Newsletter(Newsletter newsletter)
        {
            _db.Newsletters.Add(newsletter);
            _db.SaveChanges();
            return RedirectToAction("Index", "Main");
        }

        //Get - AUTHENTICATED Newsletter for ADMIN Page
        public IActionResult NewsAdmin()
        {
            return View(_db.Newsletters.ToList());
        }

        // Post - LogOff
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
