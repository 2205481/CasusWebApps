using System.ComponentModel;
using CasusWebApps.Models;
using Microsoft.AspNetCore.Http;


namespace CasusWebApps.Models.ViewModels
{
    public class AddImageRequest
    {
        public string ImageUrl { get; set; }
        public string ItemType { get; set; }
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}