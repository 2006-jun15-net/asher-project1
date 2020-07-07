using StoreApplication.Library.Models;
using System.Collections.Generic;

namespace StoreApplication.Library
{
    public interface IOrderHistoryRepository
    {
        Customer getCustomerRef(OrderHistory orderHistory);
        Location getLocationRef(OrderHistory orderHistory);
        OrderHistory GetById(int id);
        IEnumerable<OrderHistory> GetAllCustomerOrders(Customer customer);
        IEnumerable<OrderHistory> GetAllLocationOrders(Location location);
    }
}
