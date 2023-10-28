using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace FurnitureStore.Models
{
    public partial class Image
    {
        public decimal Id { get; set; }
        [DisplayName("Image")]
        public string Imagepath { get; set; }
        [DisplayName("Furniture")]
        public decimal FurnitureId { get; set; }
        [NotMapped,DisplayName("Image")]
        public IFormFile ImageFile { get; set; }
        public virtual Furniture Furniture { get; set; }
    }
}
