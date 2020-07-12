using System;
using System.Collections.Generic;

namespace StoreApplication.Library.Models
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? LocationId { get; set; }
        public DateTime TimeOrdered { get; set; }

        public List<Order> orders = new List<Order>();
    }
}
