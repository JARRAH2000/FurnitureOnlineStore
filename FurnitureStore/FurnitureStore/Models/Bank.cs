
#nullable disable

namespace FurnitureStore.Models
{
    public partial class Bank
    {
        public decimal Id { get; set; }
        public string CardNumber { get; set; }
        public string CardCvc { get; set; }
        public string ExpirationDate { get; set; }
        public decimal Balance { get; set; }
        public string FullName { get; set; }
    }
}
