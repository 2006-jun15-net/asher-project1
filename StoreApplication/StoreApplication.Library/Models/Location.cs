using System;
using System.Collections.Generic;

namespace StoreApplication.Library.Models
{
    public class Location
    {
        private string _address;
        private string _city;
        private string _state;

        public int Id { get; set; }

        public string Address
        {
            get => _address;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("Address cannot be left blank", nameof(value));
                }
                _address = value;
            }
        }

        public string City
        {
            get => _city;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("City cannot be left blank", nameof(value));
                }
                _city = value;
            }
        }

        public string State
        {
            get => _state;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("State cannot be left blank", nameof(value));
                }
                _state = value;
            }
        }

        public List<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}
