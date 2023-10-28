using System;
using System.Collections.Generic;

#nullable disable

namespace FurnitureStore.Models
{
    public partial class Testimonial
    {
        public decimal Id { get; set; }
        public string Text { get; set; }
        public DateTime SendTime { get; set; }
        public string Publish { get; set; }
        public decimal SenderId { get; set; }

        public virtual User Sender { get; set; }
    }
}
