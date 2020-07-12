using System;

namespace StoreApplication.Library.Models
{
    public class Product
    {
        private string _name;
        private decimal _price;
        private int _maxPerOrder;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("Name cannot be empty", nameof(value));
                }
                _name = value;
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Price cannot be a negative value", nameof(value));
                }
                _price = value;
            }
        }

        public int MaxPerOrder
        {
            get => _maxPerOrder;
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Value cannot be negative", nameof(value));
                }
                _maxPerOrder = value;
            }
        }
    }
}
