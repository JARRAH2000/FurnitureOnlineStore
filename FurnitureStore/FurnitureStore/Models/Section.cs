using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FurnitureStore.Models
{
    public partial class Section
    {
        public Section()
        {
            Furnitures = new HashSet<Furniture>();
        }

        public decimal Id { get; set; }
        [DisplayName("Section Name")]
        public string Name { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }
        [NotMapped, DisplayName("Image")]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<Furniture> Furnitures { get; set; }
    }
}
