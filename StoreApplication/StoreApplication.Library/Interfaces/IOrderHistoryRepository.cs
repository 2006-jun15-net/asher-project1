using StoreApplication.Library.Models;
using System.Collections.Generic;

namespace StoreApplication.Library
{
    public interface IOrderHistoryRepository
    {
        Customer getCustomerRef(OrderHistory orderHistory);
        Location getLocationRef(OrderHistory orderHistory);
        OrderHistory GetById(int id);
        IEnumerable<OrderHistory> GetAllCustomerOrders(int customerId);
        IEnumerable<OrderHistory> GetAllLocationOrders(int locationId);
        void AddOrderHistory(OrderHistory orderHistory);
        void Save();
        int GetLatestOrderHistory(int customerId);
    }
}
