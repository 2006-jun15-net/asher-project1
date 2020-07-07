using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApplication.Library.Models
{
    public class Customer
    {
        private string _firstName;
        private string _lastName;
        private string _userName;

        public int Id { get; set; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("First Name cannot be empty", nameof(value));
                }
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Last Name cannot be empty", nameof(value));
                }
                _lastName = value;
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Username cannot be empty", nameof(value));
                }
                _userName = value;
            }
        }

        public List<OrderHistory> Orders { get; set; } = new List<OrderHistory>();
    }
}
