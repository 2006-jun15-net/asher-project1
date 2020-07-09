using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private Project0StoreContext context = null;
        private DbSet<Product> table = null;

        public ProductRepository()
        {
            this.context = new Project0StoreContext();
            table = context.Set<Product>();
        }

        public ProductRepository(Project0StoreContext _context)
        {
            this.context = _context;
            table = _context.Set<Product>();
        }

        public IEnumerable<StoreApplication.Library.Models.Product> GetAll()
        {
            var products = context.Product.ToList();
            return products.Select(Mapper.MapProductEntity);
        }

        public StoreApplication.Library.Models.Product GetById(object id)
        {
            return Mapper.MapProductEntity(context.Product.Find(id));
        }

        public void Insert(StoreApplication.Library.Models.Product obj)
        {
            Product entity = Mapper.MapProductDTO(obj);
            context.Add(entity);
        }

        public void Update(StoreApplication.Library.Models.Product obj)
        {
            Product currentEntity = context.Product.Find(obj.Id);
            Product newEntity = Mapper.MapProductDTO(obj);

            context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        public void Delete(object id)
        {
            Product entity = context.Product.Find(id);
            context.Product.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public int FindProduct(string name)
        {
            Product entity = context.Product.FirstOrDefault(p => p.Name == name);
            return entity.ProductId;
        }

        public bool ExceedMaxAmount(int amountOrdered, int id)
        {
            Product entity = context.Product.Find(id);
            
            if(entity.MaxPerOrder < amountOrdered)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
}
