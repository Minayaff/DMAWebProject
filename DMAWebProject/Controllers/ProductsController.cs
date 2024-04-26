using Microsoft.AspNetCore.Mvc;

namespace DMAWebProject.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
