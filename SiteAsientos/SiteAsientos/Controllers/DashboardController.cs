using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteAsientos.Models;

namespace SiteAsientos.Controllers
{
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
    }
}
