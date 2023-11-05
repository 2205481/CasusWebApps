namespace CasusWebApps.Models
{
    public class ImageTag
    {
        public Guid Id { get; set; }
        public float TypeWindow { get; set; }

        /* Content set in public enum ImageItemType, see below */
        public ImageItemType ObjectType { get; set; }

        /* Foreign key ImageHandler */
        public Guid ImageHandlerId { get; set; }

        /* Nav property  for relationship with ImageHandler */
        public ImageHandler ImageHandler { get; set; }
    }

    public enum ImageItemType
    {
        Bak, Bloemenbak, Buis, DVDhoes, Emmer, Gieter, Jerrycan,
        Kabel, Klerenhanger, Kozijn, Krat, Lp, Tuinstoel, Vat,
    }
}
