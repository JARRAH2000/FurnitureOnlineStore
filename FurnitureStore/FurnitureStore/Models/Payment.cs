using System;
using System.Collections.Generic;

#nullable disable

namespace FurnitureStore.Models
{
    public partial class Payment
    {
        public Payment()
        {
            ProductPayments = new HashSet<ProductPayment>();
        }
        public decimal Id { get; set; }
        public DateTime BillTime { get; set; }
        public decimal UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProductPayment> ProductPayments { get; set; }
    }
}
