using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace FurnitureStore.Models
{
    public partial class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
            Bags = new HashSet<Bag>();
            Comments = new HashSet<Comment>();
            Favourites = new HashSet<Favourite>();
            Payments = new HashSet<Payment>();
            Ratings = new HashSet<Rating>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal Id { get; set; }
        [DisplayName("First Name"),Required]
        public string Firstname { get; set; }
        [DisplayName("Last Name"),Required]

        public string Lastname { get; set; }
        [DisplayName("Birth Date"),Required]
        public DateTime? Birthdate { get; set; }
        [Required]
        public string Sex { get; set; }
        [DisplayName("Picture")]
        public string Imagepath { get; set; }
        [NotMapped, DisplayName("Picture")]
        public IFormFile ImageFile { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Bag> Bags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
