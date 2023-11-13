using CasusWebApps.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using CasusWebApps.Models;
using CasusWebApps.Migrations;
using Microsoft.Extensions.Hosting;
using System.IO.Compression;

namespace CasusWebApps.Controllers
{
    public class ProcessedImagesController : Controller
    {
        private readonly WasteDbContext wasteDbContext;

        public ProcessedImagesController(WasteDbContext db)
        {
            this.wasteDbContext = db;
        }

        public ActionResult Index()
        {
            var imageHandler = wasteDbContext.AnnotationModels.ToList();
            return View("Index", imageHandler);
        }

        // GET: ImageUploadController1/Delete/5
        public ActionResult Delete(Guid id)
        {
            var product = wasteDbContext.AnnotationModels.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ImageUploadController1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteImage(Guid id)
        {
            var product = wasteDbContext.AnnotationModels.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            wasteDbContext.AnnotationModels.Remove(product);
            wasteDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ImageUploadController1/Details/5
        public ActionResult Details(Guid id)
        {
            var product = wasteDbContext.AnnotationModels.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ImageUploadController1/Details/5
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsImage(Guid id)
        {
            var product = wasteDbContext.AnnotationModels.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");

        }

        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAll()
        {
            var allAnnotations = await wasteDbContext.AnnotationModels.ToListAsync();
            wasteDbContext.AnnotationModels.RemoveRange(allAnnotations);

            await wasteDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
