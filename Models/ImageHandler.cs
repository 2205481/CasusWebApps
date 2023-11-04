namespace CasusWebApps.Models
{
    public class ImageHandler
    {
        public Guid Id {  get; set; }
        public string ImageUrl { get; set; }
        public string ItemType { get; set; }

        /* Defines one to many relationship between ImageHandler and ImageTags*/
        public ICollection<ImageTag> ImageTags { get; set; } 

    }
}
