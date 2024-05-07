using DMAWebProject.DAL;
using DMAWebProject.Extensions;
using DMAWebProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMAWebProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly IWebHostEnvironment _env;

        public ProductsController(AppDbContext _appDbContext, IWebHostEnvironment env)
        {
            _env = env;
            appDbContext = _appDbContext;
        }
        public IActionResult Index()
        {
            return View(appDbContext.Products.Include(x => x.Category).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Category = appDbContext.Categories.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Products model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Category = appDbContext.Categories.ToList();
                return View(model);
            }

            //TEK SEKILIN BAZAYA HEMDE PAPKAYA YUKLENMESI
            if (!model.ImgUrlBaseFile.IsImage())
            {
                ModelState.AddModelError("Photo", "Image type is not valid");
                return View(model);
            }
            string filename = await model.ImgUrlBaseFile.SaveFileAsync(_env.WebRootPath, "uploadProducts");

            model.ImgUrlBase = filename;

            appDbContext.Products.Add(model);
            appDbContext.SaveChanges();


            if (model.ImagesFiles != null)
            {
                foreach (var item in model.ImagesFiles)
                {
                    if (!item.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Image type is not valid");
                        return View(model);
                    }
                    string filename2 = await item.SaveFileAsync(_env.WebRootPath, "uploadProducts");

                    Images images = new Images
                    {
                        ProductId = model.Id,
                        ImgUrl = filename2
                    };
                    appDbContext.Images.Add(images);
                    appDbContext.SaveChanges();

                }

            }


            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Category = appDbContext.Categories.ToList();
            var model = appDbContext.Products.Include(x=>x.Images).FirstOrDefault(x => x.Id == id);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Products productModel)
        {
            ViewBag.Category = appDbContext.Categories.ToList();
            var modelDb = appDbContext.Products.FirstOrDefault(x=>x.Id==productModel.Id);
            modelDb.Title = productModel.Title;
            modelDb.Price = productModel.Price;
            modelDb.CategoryId = productModel.CategoryId;
            modelDb.Description = productModel.Description;
            appDbContext.SaveChanges();

            if (modelDb == null)
            {
                return RedirectToAction("Index");
            }

            if (productModel.ImgUrlBaseFile != null)
            {
                if (!productModel.ImgUrlBaseFile.IsImage())
                {
                    ModelState.AddModelError("Photo", "Image type is not valid");
                    return View(productModel);
                }
                string filename = await productModel.ImgUrlBaseFile.SaveFileAsync(_env.WebRootPath, "uploadProducts");

                modelDb.ImgUrlBase = filename;
                appDbContext.SaveChanges();
            }
          

            if (productModel.ImagesFiles !=null)
            {
                foreach (var item in productModel.ImagesFiles)
                {
                    if (!item.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Image type is not valid");
                        return View(productModel);
                    }
                    string filename2 = await item.SaveFileAsync(_env.WebRootPath, "uploadProducts");

                    Images images = new Images
                    {
                        ProductId = productModel.Id,
                        ImgUrl = filename2
                    };
                    appDbContext.Images.Add(images);
                    appDbContext.SaveChanges();

                }
            }

            return RedirectToAction("Index");

        }

        public IActionResult DeleteImage(int id)
        {
            if (id!=0)
            {
               var model= appDbContext.Images.Find(id);
                appDbContext.Images.Remove(model);
                appDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
