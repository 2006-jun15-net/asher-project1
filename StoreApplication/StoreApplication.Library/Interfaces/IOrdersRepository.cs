using StoreApplication.Library.Models;
using System.Collections.Generic;

namespace StoreApplication.Library
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> GetAllOrdersInHistory();
        double CalculateTotal(Product product, Order orders);
    }
}
