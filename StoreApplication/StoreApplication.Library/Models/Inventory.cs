using System;

namespace StoreApplication.Library.Models
{
    public class Inventory
    {
        private int _inStock;

        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? LocationId { get; set; }

        public int InStock
        {
            get => _inStock;
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Stock cannot be negative", nameof(value));
                }
                _inStock = value;
            }
        }
    }
}
