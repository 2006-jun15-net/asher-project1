using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? OrderHistoryId { get; set; }
        public int? ProductId { get; set; }
        public int? AmountOrdered { get; set; }

    }
}
