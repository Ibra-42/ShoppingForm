using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingFormApp.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? WorkerId { get; set; }
        public int? Count { get; set; }
        public DateTime? Date { get; set; }
        public decimal? TotalPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
