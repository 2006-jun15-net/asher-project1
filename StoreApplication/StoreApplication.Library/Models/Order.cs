using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class Order
    {
        private string _productName;
        private int? _amountOrdered;

        public int Id { get; set; }
        public int? OrderHistoryId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }

        public int? AmountOrdered 
        {
            get => _amountOrdered; 
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Cannot order a negative amount", nameof(value));
                }
                _amountOrdered = value;
            }
        }

    }
}
