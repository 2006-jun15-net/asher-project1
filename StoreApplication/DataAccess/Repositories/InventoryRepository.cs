using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class InventoryRepository : IInventoryRepository
    {
        private Project0StoreContext context = null;
        private DbSet<Inventory> table = null;

        public InventoryRepository()
        {
            context = new Project0StoreContext();
            table = context.Set<Inventory>();
        }
        public InventoryRepository(Project0StoreContext context)
        {
            this.context = context;
            table = this.context.Set<Inventory>();
        }

        public StoreApplication.Library.Models.Inventory FindLocationInventory(StoreApplication.Library.Models.Location location, StoreApplication.Library.Models.Product product)
        {
            Location locationEntity = context.Location.Find(location.Id);
            Product productEntity = context.Product.Find(product.Id);
            return Mapper.MapInventoryEntities(context.Inventory
                .Where(i => (i.Location == locationEntity) && (i.Product == productEntity)).FirstOrDefault());
        }

        public void Update(StoreApplication.Library.Models.Inventory obj)
        {
            Inventory currentEntity = context.Inventory.Find(obj.Id);
            Inventory newEntity = Mapper.MapInventoryDTO(obj);
            context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }
    }
}
