namespace CasusWebApps.Models
{
    public class ImageTag
    {
        public Guid Id { get; set; }
        public string ObjectType { get; set; }
        public float TypeWindow { get; set; }
        
        /* Foreign key ImageHandler */
        public Guid ImageHandlerId { get; set; }

        /* Nav property  for relationship with ImageHandler */ 
        public ImageHandler ImageHandler { get; set; }
    }
}
