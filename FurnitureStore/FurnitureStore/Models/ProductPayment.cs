#nullable disable

namespace FurnitureStore.Models
{
    public partial class ProductPayment
    {
        public decimal Id { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? PaymentId { get; set; }
        public decimal? FurnitureId { get; set; }
        public virtual Furniture Furniture { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
