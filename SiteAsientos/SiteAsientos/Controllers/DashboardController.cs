using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteAsientos.Extensions;
using SiteAsientos.Models;

namespace SiteAsientos.Controllers
{
    [Authorize(Roles = "Administrador,Empleado")]
    public class DashboardController : Controller
    {
        private readonly CubreasientosContext _context;

        public DashboardController(CubreasientosContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Tables()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View(HttpContext.Session.ObtenerUsuario<Employee>("user"));
        }
    }
}
