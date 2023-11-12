using CasusWebApps.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using CasusWebApps.Models;
using CasusWebApps.Migrations;

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
            var imageHandler = new CasusWebApps.Models.ImageHandler
            {
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

                imageHandler.ImageUrl = "/uploads/" + uniqueFileName;
            }
            wasteDbContext.ImageHandlers.Add(imageHandler);
            wasteDbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        // GET: ImageUploadController1
        public ActionResult Index()
        {
            var imageHandler = wasteDbContext.ImageHandlers.ToList();
            return View("AllImages", imageHandler);
        }

        public IActionResult ProcessImage()
        {
            var images = wasteDbContext.ImageHandlers.ToList();

            if (images.Any())
            {
                return View("ProcessImage", images);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("ImageUpload/GetImageById")]
        public IActionResult GetImageById(Guid id)
        {
            var image = wasteDbContext.ImageHandlers.FirstOrDefault(i => i.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            return File(System.IO.Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", image.ImageUrl), "image/jpeg");
        }

        [HttpPost]
        public IActionResult SaveAnnotation(AnnotationModel annotation)
        {
            if (ModelState.IsValid)
            {
                string processedUploadsfolder = System.IO.Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "processed-uploads");
                string uniqueCanvasFileName = Guid.NewGuid().ToString() + "_processed_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                string canvasFilePath = System.IO.Path.Combine(processedUploadsfolder, uniqueCanvasFileName);

                SaveCanvasImage(annotation.CanvasImage, canvasFilePath);
                annotation.ImageUrl = "/processed-uploads/" + uniqueCanvasFileName;

                wasteDbContext.AnnotationModels.Add(annotation);
                wasteDbContext.SaveChanges();

                return Ok();
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }

                return BadRequest();
            }

            return BadRequest();
        }

        private void SaveCanvasImage(string canvasImageData, string filePath)
        {
            byte[] canvasImageBytes = Convert.FromBase64String(canvasImageData.Split(',')[1]);
            System.IO.File.WriteAllBytes(filePath, canvasImageBytes);
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
        public ActionResult Delete(Guid id)
        {
            var product = wasteDbContext.ImageHandlers.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ImageUploadController1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteImage(Guid id, string file)
        {
            var product = wasteDbContext.ImageHandlers.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            wasteDbContext.ImageHandlers.Remove(product);
            wasteDbContext.SaveChanges();


            return RedirectToAction("Index");

        }

        // GET: ImageUploadController1/Delete/5
        public ActionResult Details(Guid id)
        {
            var product = wasteDbContext.ImageHandlers.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

            // POST: ImageUploadController1/Delete/5
            [HttpPost, ActionName("Details")]
            [ValidateAntiForgeryToken]
            public ActionResult DetailsImage(Guid id)
            {
                var product = wasteDbContext.ImageHandlers.Find(id);
                if (product == null)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");

            }
    }
}
