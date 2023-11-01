using CasusWebApps.Models;
using CasusWebApps.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace CasusWebApps.Controllers
{
    public class ImageUploadController : Controller
    {
        private readonly WasteDbContext wasteDbContext;
        private readonly IHostEnvironment hostEnvironment;

        public ImageUploadController(WasteDbContext wasteDbContext, IHostEnvironment hostEnvironment)
        {
            this.wasteDbContext = wasteDbContext;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("AddImage");
        }

        // POST: ImageUploadController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddImageRequest addImageRequest)
        {
            var imageHandler = new ImageHandler
            {
                ItemType = addImageRequest.ItemType
            };

            if (addImageRequest.ImageFile != null && addImageRequest.ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + addImageRequest.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    addImageRequest.ImageFile.CopyTo(stream);
                }

                    addImageRequest.ImageUrl = "/uploads/" + uniqueFileName;
            }
            wasteDbContext.ImageHandlers.Add(imageHandler);
            wasteDbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        /*
        // GET: ImageUploadController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: ImageUploadController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ImageUploadController1/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // GET: ImageUploadController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ImageUploadController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ImageUploadController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ImageUploadController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        */
    }
    
}
