using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FurnitureStore.Models
{
    public partial class ContactInfo
    {
        public decimal Id { get; set; }
        [MaxLength(15,ErrorMessage ="Max length of a telephone number is 15 digits")]
        public string Phone { get; set; }
        [MaxLength(100, ErrorMessage = "Max length of an email is 100 character")]

        public string Email { get; set; }
        [MaxLength(500,ErrorMessage = "Max length of a url is 500 character")]
        public string Facebook { get; set; }
        [MaxLength(500, ErrorMessage = "Max length of a url is 500 character")]

        public string Instagram { get; set; }
        [MaxLength(500, ErrorMessage = "Max length of a url is 500 character")]

        public string Twitter { get; set; }
        [MaxLength(500, ErrorMessage = "Max length of a url is 500 character")]

        public string Youtube { get; set; }
        [MaxLength(100, ErrorMessage = "Max length of a fax number is 100 digits")]
        [DisplayName("LinkedIn")]
        public string Fax { get; set; }
        [MaxLength(100, ErrorMessage = "Max length of an address is 100 character")]

        public string Address { get; set; }
    }
}
