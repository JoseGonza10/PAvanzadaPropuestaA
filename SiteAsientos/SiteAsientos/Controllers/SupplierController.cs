using Microsoft.AspNetCore.Mvc;

namespace SiteAsientos.Controllers
{
	public class SupplierController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
