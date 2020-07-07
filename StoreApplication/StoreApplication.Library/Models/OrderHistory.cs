using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? LocationId { get; set; }
        public DateTime TimeOrdered { get; set; }
    }
}
