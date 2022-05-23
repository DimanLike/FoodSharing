using Microsoft.AspNetCore.Mvc;

namespace FoodSharing.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ProductInfo()
		{
            return View();
        }


    }
}
