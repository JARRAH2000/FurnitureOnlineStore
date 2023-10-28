using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureStore.Models
{
	public partial class Pages
	{
		public decimal Id { get; set; }
		public string About { get; set; }
		[DisplayName("About Image")]
		public string AboutImagePath { get; set; }
        [NotMapped, DisplayName("About Image")]
        public IFormFile AboutImage { get; set; }
		[DisplayName("Main Logo")]
        public string MainLogoPath { get; set; }
		[NotMapped, DisplayName("Main Logo")]
		public IFormFile MainLogo { get; set; }
		[DisplayName("Second greeting")]
		public string SecLogoPath { get; set; }
		[NotMapped, DisplayName("Second Logo")]
        public string TopImagePath { get; set; }
        [NotMapped,DisplayName("Log in Image")]
        public IFormFile TopImage { get; set; }
		[DisplayName("Home greeting")]
		public string Greeting { get; set; }
		
	}
}
