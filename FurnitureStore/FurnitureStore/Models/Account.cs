using System.ComponentModel;

#nullable disable

namespace FurnitureStore.Models
{
    public partial class Account
    {
        public decimal Id { get; set; }
        [DisplayName("Email")]
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal UserId { get; set; }
        public decimal RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
