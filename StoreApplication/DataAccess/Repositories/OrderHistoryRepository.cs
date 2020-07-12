using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Library;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private Project0StoreContext context;
        private DbSet<OrderHistory> table = null;

        public OrderHistoryRepository()
        {
            context = new Project0StoreContext();
            table = context.Set<OrderHistory>();
        }
        public OrderHistoryRepository(Project0StoreContext context)
        {
            this.context = context;
            table = context.Set<OrderHistory>();
        }

        public IEnumerable<StoreApplication.Library.Models.OrderHistory> GetAllCustomerOrders(int customerId)
        {
            Customer customerEntity = context.Customer.Find(customerId);
            var customerHistories = context.OrderHistory
                .Where(o => o.CustomerId == customerEntity.CustomerId).ToList();

            return customerHistories.Select(Mapper.MapOrderHistoryEntity);
        }

        public IEnumerable<StoreApplication.Library.Models.OrderHistory> GetAllLocationOrders(int locationId)
        {
            Location locationEntity = context.Location.Find(locationId);
            var locationHistories = context.OrderHistory
                .Where(o => o.LocationId == locationEntity.LocationId).ToList();

            return locationHistories.Select(Mapper.MapOrderHistoryEntity);
        }

        public StoreApplication.Library.Models.OrderHistory GetById(int id)
        {
            return Mapper.MapOrderHistoryEntity(context.OrderHistory.Find(id));
        }

        public StoreApplication.Library.Models.Customer getCustomerRef(StoreApplication.Library.Models.OrderHistory orderHistory)
        {
            OrderHistory historyEntity = context.OrderHistory.Find(orderHistory.Id);
            var customerRef = context.OrderHistory
                .Where(o => o.OrderHistoryId == historyEntity.OrderHistoryId)
                .Include(c => c.Customer).FirstOrDefault();

            Customer entity = customerRef.Customer;
            return Mapper.MapCustomerEntity(entity);
        }

        public StoreApplication.Library.Models.Location getLocationRef(StoreApplication.Library.Models.OrderHistory orderHistory)
        {
            OrderHistory historyEntity = context.OrderHistory.Find(orderHistory.Id);
            var locationRef = context.OrderHistory
                .Where(o => o.OrderHistoryId == historyEntity.OrderHistoryId)
                .Include(l => l.Location).FirstOrDefault();

            Location entity = locationRef.Location;
            return Mapper.MapLocationEntities(entity);
        }

        public void AddOrderHistory(StoreApplication.Library.Models.OrderHistory orderHistory)
        {
            OrderHistory entity = Mapper.MapOrderHistoryDTO(orderHistory);
            //entity.OrderHistoryId = 0;
            context.OrderHistory.Add(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public int GetLatestOrderHistory(int customerId)
        {
            var histories = GetAllCustomerOrders(customerId).ToList();
            return histories[histories.Count - 1].Id;
        }
    }
}
