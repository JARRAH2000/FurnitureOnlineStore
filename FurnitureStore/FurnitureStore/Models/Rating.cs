#nullable disable

namespace FurnitureStore.Models
{
    public partial class Rating
    {
        public decimal Id { get; set; }
        public decimal? Stars { get; set; }
        public decimal UserId { get; set; }
        public decimal FurnitureId { get; set; }

        public virtual Furniture Furniture { get; set; }
        public virtual User User { get; set; }
    }
}
