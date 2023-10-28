using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace FurnitureStore.Models
{
    public partial class Furniture
    {
        public Furniture()
        {
            Bags = new HashSet<Bag>();
            Comments = new HashSet<Comment>();
            Favourites = new HashSet<Favourite>();
            Images = new HashSet<Image>();
            Offers = new HashSet<Offer>();
            ProductPayments = new HashSet<ProductPayment>();
            Ratings = new HashSet<Rating>();
        }

        public decimal Id { get; set; }
        [MaxLength(200, ErrorMessage = "Name length must be less than or equal 200 characters")]
        public string Name { get; set; }
        [Range(0, 1_000_000, ErrorMessage = "Quantity must by non-negative integer less than or equal to 1 million")]
        public decimal? Quantity { get; set; }
        [Range(1, 1000_000, ErrorMessage = "Price  must be non negative number less than or equal to 1 million"),Required,NotNullAttribute]
        public decimal Price { get; set; }
        [MaxLength(500, ErrorMessage = "Description length must be less than or equal to 500 characters")]
        public string Description { get; set; }
        [DisplayName("Image"), MaxLength(100, ErrorMessage = "Image name length must ne less than or equal 60 characters")]
        public string Imagepath { get; set; }
        [NotMapped,DisplayName("Image")]
        public IFormFile ImageFile { get; set; }
        [DisplayName("Section")]
        public decimal SectionId { get; set; }
        [Range(1.0, 10000000, ErrorMessage = "Cost must positive number less than or equal to 1 million")]
       
        public decimal? Cost { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<Bag> Bags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<ProductPayment> ProductPayments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
