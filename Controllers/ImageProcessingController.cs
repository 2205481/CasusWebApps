using Microsoft.AspNetCore.Mvc;
using System.IO;
using CasusWebApps.Models;
using Microsoft.Extensions.Hosting;
using CasusWebApps.Migrations;

namespace CasusWebApps.Controllers
{
    public class ImageProcessingController : Controller
    {
        private readonly WasteDbContext wasteDbContext;
        private readonly IHostEnvironment hostEnvironment;

        public ImageProcessingController(WasteDbContext wasteDbContext, IHostEnvironment hostEnvironment)
        {
            this.wasteDbContext = wasteDbContext;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: ImageUploadController1/Delete/5
        public ActionResult Details(Guid id)
        {
            var annotated = wasteDbContext.AnnotationModels.Find(id);

            return View("Details", annotated);
        }

        // POST: ImageUploadController1/Delete/5
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsImage1(Guid id)
        {
            var annotated = wasteDbContext.AnnotationModels.Find(id);
            if (annotated == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");

        }

		public IActionResult DetailImage2()
		{
			var images = wasteDbContext.AnnotationModels.ToList();

			if (images.Any())
			{
				return View("Details", images);
			}
			return NotFound();
		}

		// GET: ImageUploadController1
		public ActionResult Index()
        {
            var annotationModel = wasteDbContext.AnnotationModels.ToList();
            return View("Index", annotationModel);
        }

    }
}