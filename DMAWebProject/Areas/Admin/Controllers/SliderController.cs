using DMAWebProject.Controllers;
using DMAWebProject.DAL;
using DMAWebProject.Extensions;
using DMAWebProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DMAWebProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext _appDbContext, IWebHostEnvironment env)
        {
            appDbContext = _appDbContext;
            _env=env;
        }

        public IActionResult Index()
        {

            return View(appDbContext.Sliders.Where(x=>x.IsActive==true).ToList());
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {

            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            if (!slider.file.IsImage())
            {
                ModelState.AddModelError("Photo", "Image type is not valid");
                return View(slider);
            }
            string filename =  await slider.file.SaveFileAsync(_env.WebRootPath, "uploadSlider");

            slider.ImgUrl = filename;   

            appDbContext.Sliders.Add(slider);
            appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            if (id == 0)
            {
                return Json(new
                {
                    status = 400
                });
            }

            var slider = appDbContext.Sliders.Find(id); //axtarib tapiram
            if (slider!=null)
            {
                slider.IsActive = false; ;
                appDbContext.SaveChanges();
            }

            return Json(new
            {
                status = 200
            });

        }

        [HttpGet]
        public JsonResult Edit(int id)
        {
            if (id==0)
            {
                return Json(new
                {
                    status = 400
                });
            }
            var model=appDbContext.Sliders.FirstOrDefault(x=>x.Id == id);
            if (model==null)
            {
                return Json(new
                {
                    status = 400
                });
            }
            return Json(model);
        }

        [HttpPost]
        public IActionResult Edit(Slider slider)
        {

            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            appDbContext.Sliders.Update(slider); 
            appDbContext.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}
