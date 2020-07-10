using StoreApplication.Library.Models;
using System;
using Xunit;

namespace StoreApplication.Testing
{
    public class OrderTest
    {
        private readonly Order order = new Order();

        [Fact]
        public void AmountOrdered_AboveZero_StoresCorrectly()
        {
            int randomValue = 52;
            order.AmountOrdered = randomValue;
            Assert.True(order.AmountOrdered == randomValue);
        }

        [Fact]
        public void ThrowsException_WhenPassed_NegativeValue()
        {
            Assert.ThrowsAny<ArgumentException>(() => order.AmountOrdered = -23);
        }
    }
}
