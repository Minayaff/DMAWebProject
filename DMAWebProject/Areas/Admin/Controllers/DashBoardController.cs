using DMAWebProject.DAL;
using DMAWebProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DMAWebProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        private readonly AppDbContext appDbContext;

        public DashBoardController(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            slider.ImgUrl = "test";

            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            appDbContext.Sliders.Add(slider);
            appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
