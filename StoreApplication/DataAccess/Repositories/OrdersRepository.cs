﻿using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Library;
using StoreApplication.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class OrdersRepository : IOrdersRepository
    {
        private Project0StoreContext context = null;
        private DbSet<Orders> table = null;

        public OrdersRepository()
        {
            context = new Project0StoreContext();
            table = context.Set<Orders>();
        }
        public OrdersRepository(Project0StoreContext context)
        {
            this.context = context;
            table = this.context.Set<Orders>();
        }

        public double CalculateTotal(StoreApplication.Library.Models.Product product, StoreApplication.Library.Models.Order orders)
        {
            double sum = (double)(product.Price * orders.AmountOrdered);
            return sum;
        }

        public IEnumerable<StoreApplication.Library.Models.Order> GetAllOrdersInHistory()
        {
            var orders = context.Orders.Include(oh => oh.OrderHistory);
            return orders.Select(Mapper.MapOrdersEntity);
        }

        public void AddOrder(StoreApplication.Library.Models.Order order)
        {
            Orders entity = Mapper.MapOrdersDTO(order);
            context.Orders.Add(entity);
        }
    }
}