using StoreApplication.Library.Models;
using System;
using Xunit;

namespace StoreApplication.Testing
{
    public class CustomerTest
    {
        private readonly Customer customer = new Customer();

        [Fact]
        public void FirstName_NonEmptyValue_StoreCorrectly()
        {
            string randomName = "Jacob";
            customer.FirstName = randomName;
            Assert.Equal(randomName, customer.FirstName);
        }

        [Fact]
        public void FirstName_ThrowsException_WhenEmpty()
        {
            Assert.ThrowsAny<ArgumentException>(() => customer.FirstName = String.Empty);
        }

        [Fact]
        public void LastName_NonEmptyValue_StoresCorrectly()
        {
            string randomName = "Wilcox";
            customer.LastName = randomName;
            Assert.Equal(randomName, customer.LastName);
        }

        [Fact]
        public void LastName_ThrowsException_WhenEmpty()
        {
            Assert.ThrowsAny<ArgumentException>(() => customer.LastName = String.Empty);
        }

        [Fact]
        public void UserName_NonEmptyValue_StoresCorrectly()
        {
            string randomName = "RapidMan523!?";
            customer.UserName = randomName;
            Assert.Equal(randomName, customer.UserName);
        }

        [Fact]
        public void UserName_ThrowsException_WhenEmpty()
        {
            Assert.ThrowsAny<ArgumentException>(() => customer.UserName = String.Empty);
        }
    }
}
