using System;
using System.ComponentModel;

#nullable disable

namespace FurnitureStore.Models
{
    public partial class Offer
    {
        public decimal Id { get; set; }
        public decimal Percentage { get; set; }
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        public decimal FurnitureId { get; set; }
        public string Description { get; set; }
        public virtual Furniture Furniture { get; set; }
    }
}
