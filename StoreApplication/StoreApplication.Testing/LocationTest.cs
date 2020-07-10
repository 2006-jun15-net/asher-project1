using StoreApplication.Library.Models;
using System;
using Xunit;

namespace StoreApplication.Testing
{
    public class LocationTest
    {
        private readonly Location location = new Location();

        [Fact]
        public void Address_NonEmptyValue_StoresCorrectly()
        {
            string randomAddress = "5345 Willow Rd";
            location.Address = randomAddress;
            Assert.Equal(randomAddress, location.Address);
        }

        [Fact]
        public void ThrowsException_WhenAddressEmpty()
        {
            Assert.ThrowsAny<ArgumentException>(() => location.Address = String.Empty);
        }

        [Fact]
        public void City_NonEmptyValue_StoresCorrectly()
        {
            string randomCity = "Seattle";
            location.City = randomCity;
            Assert.Equal(randomCity, location.City);
        }

        [Fact]
        public void ThrowsException_WhenCityEmpty()
        {
            Assert.ThrowsAny<ArgumentException>(() => location.City = String.Empty);
        }

        [Fact]
        public void State_NonEmptyValue_StoresCorrectly()
        {
            string randomState = "Virginia";
            location.State = randomState;
            Assert.Equal(randomState, location.State);
        }

        [Fact]
        public void ThrowsException_WhenStateEmpty()
        {
            Assert.ThrowsAny<ArgumentException>(() => location.State = String.Empty);
        }
    }
}
