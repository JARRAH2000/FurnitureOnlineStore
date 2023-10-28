using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureStore.Models
{
    public partial class Message
    {
        public decimal Id { get; set; }
        [Required(ErrorMessage = "Please enter a subject for your message"),MaxLength(100)]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter your message"), DisplayName("Message"),MaxLength(500)]
        public string Text { get; set; }
        [DisplayName("Image"),MaxLength(100)]
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [DisplayName("Time")]
        public DateTime SendTime { get; set; }
        [Required(ErrorMessage = "Please enter your email"),MaxLength(500)]
        public string Email { get; set; }
    }
}
