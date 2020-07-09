using StoreApplication.Library.Models;
using System.Collections.Generic;

namespace StoreApplication.Library
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(object id);
        void Insert(Product obj);
        void Update(Product obj);
        void Delete(object id);
        void Save();
        int FindProduct(string name);
        bool ExceedMaxAmount(int amountOrdered, int id);
    }
}
