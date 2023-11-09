using System.ComponentModel.DataAnnotations;

namespace CasusWebApps.Models
{
    public class AnnotationModel
    {
        [Key]
        public Guid Id { get; set; }
        public string ItemType {  get; set; }
        public string? ImageUrl { get; set; }
        public string CanvasImage { get; set; }
        public int BoundingBoxX { get; set; }
        public int BoundingBoxY { get; set; }
        public int BoundingBoxWidth { get; set; }
        public int BoundingBoxHeight { get; set; }
    }
}
