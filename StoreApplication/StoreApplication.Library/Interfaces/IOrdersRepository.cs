using StoreApplication.Library.Models;
using System.Collections.Generic;

namespace StoreApplication.Library
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> GetAllOrdersInHistory();
        double CalculateTotal(Product product, Order orders);
        void AddOrder(Order order);
        void AddListOfOrders(List<Order> orders, StoreApplication.Library.Models.OrderHistory orderHistory);
        void Save();
        List<Order> FilterOrdersByHistory(List<Order> orders, int orderhistoryId);
    }
}
