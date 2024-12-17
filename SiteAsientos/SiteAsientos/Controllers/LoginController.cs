using Microsoft.AspNetCore.Mvc;
using SiteAsientos.Models;
using SiteAsientos.Services;
using SiteAsientos.ViewModels;

namespace SiteAsientos.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVM model)
        {
            if(_service.Login(model,this.HttpContext).Result == true)
            {
                return RedirectToAction("Index", "Supplier");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> SignOut()
        {
            if (_service.SignOut(this.HttpContext).Result == true)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                return View();
            }
        }
    }
}
