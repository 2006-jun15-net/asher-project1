using StoreApplication.Library.Models;

namespace StoreApplication.Library
{
    public interface IInventoryRepository
    {
        void Update(Inventory obj);
        Inventory FindLocationInventory(Location location, Product product);
    }
}
