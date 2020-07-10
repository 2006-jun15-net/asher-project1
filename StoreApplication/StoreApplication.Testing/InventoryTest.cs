using System;
using Xunit;
using StoreApplication.Library.Models;

namespace StoreApplication.Testing
{
    public class InventoryTest
    {
        private readonly Inventory inventory = new Inventory();

        [Fact]
        public void StockUpdates_WhenAdjusted()
        {
            int amountOrdered = 23;
            inventory.InStock = 45;
            inventory.InStock -= amountOrdered;
            Assert.True(inventory.InStock == (45 - 23));
        }

        [Fact]
        public void ThrowsException_WhenStockBelowZero()
        {
            Assert.ThrowsAny<ArgumentException>(() => inventory.InStock = -3);
        }
    }
}
