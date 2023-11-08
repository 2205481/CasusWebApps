using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Drawing.Processing;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessImage(Guid id, IFormFile editedImage, int rectangleWidth, int rectangleHeight)
        {
            var originalImage = wasteDbContext.ImageHandlers.Find(id);

            if (editedImage != null && editedImage.Length > 0)
            {
                using (var stream = editedImage.OpenReadStream())
                using (var editedImageInstance = Image.Load(stream))
                {
                    var rectangle = new RectangularPolygon(10, 10, rectangleWidth, rectangleHeight);
                    editedImageInstance.Mutate(ctx => ctx.Draw(Color.Green, 2, rectangle));

                    var uploadsFolder = System.IO.Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "processed-uploads");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + System.IO.Path.GetFileName(originalImage.ImageUrl);
                    var editedFilePath = System.IO.Path.Combine(uploadsFolder, uniqueFileName);

                    editedImageInstance.Save(editedFilePath, new JpegEncoder());
                }
            }

                return RedirectToAction("Index", "ImageUpload");
        }
    }
}