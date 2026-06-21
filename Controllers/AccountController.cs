using Microsoft.AspNetCore.Mvc;
using TempleERP.Data;

namespace TempleERP.Controllers
{
    public class AccountController : Controller
    {
        private readonly TempleDbContext _context;

        public AccountController(TempleDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(x =>
                    x.Username == username &&
                    x.Password == password);

            if (user != null)
            {
                return RedirectToAction("Index", "PoojaMasters");
            }

            ViewBag.Error = "Invalid Username or Password";

            return View();
        }

        public IActionResult Logout()
        { 
            return RedirectToAction("Login"); }
    }
}

