using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? OrderHistoryId { get; set; }
        public int? AmountOrdered { get; set; }

        public virtual OrderHistory OrderHistory { get; set; }
        public virtual Product Product { get; set; }
    }
}
