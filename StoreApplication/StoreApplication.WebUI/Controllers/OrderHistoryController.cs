using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreApplication.Library;
using StoreApplication.Library.Models;
using StoreApplication.WebUI.ViewModels;

namespace StoreApplication.WebUI.Controllers
{
    public class OrderHistoryController : Controller
    {
        public IOrderHistoryRepository OrderHistoryRepo { get; }
        public IOrdersRepository OrderRepo { get; }
        public IProductRepository ProductRepo { get; }
        public OrderHistoryController(IOrderHistoryRepository hRepo, IOrdersRepository oRepo, 
            IProductRepository pRepo)
        {
            OrderHistoryRepo = hRepo ?? throw new ArgumentException(nameof(hRepo));
            OrderRepo = oRepo ?? throw new ArgumentException(nameof(oRepo));
            ProductRepo = pRepo ?? throw new ArgumentException(nameof(pRepo));
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult LocationHistories()
        {
            int locationId = Int32.Parse(TempData["selectedLocation"].ToString());
            IEnumerable<OrderHistory> locationHistories = OrderHistoryRepo.GetAllLocationOrders(locationId);
            IEnumerable<OrderHistoryViewModel> viewModels = locationHistories.Select(x => new OrderHistoryViewModel
            {
                Id = x.Id,
                TimeOrdered = x.TimeOrdered,
            });

            return View(viewModels);
        }

        public ActionResult CustomerHistories()
        {
            int customerId = Int32.Parse(TempData["currentCustomerID"].ToString());
            TempData.Keep();
            IEnumerable<OrderHistory> customerHistories = OrderHistoryRepo.GetAllCustomerOrders(customerId);
            IEnumerable<OrderHistoryViewModel> viewModels = customerHistories.Select(x => new OrderHistoryViewModel
            {
                Id = x.Id,
                TimeOrdered = x.TimeOrdered,
            });

            return View(viewModels);
        }

        public ActionResult Details(int id)
        {
            OrderHistory orderHistory = OrderHistoryRepo.GetById(id);
            var customer = OrderHistoryRepo.getCustomerRef(orderHistory);
            string fullName = customer.FirstName + " " + customer.LastName;
            var location = OrderHistoryRepo.getLocationRef(orderHistory);
            string address = location.Address + ", " + location.City + ", " + location.State;
            List<Order> purchasedOrders = OrderRepo.GetAllOrdersInHistory().ToList();
            purchasedOrders = OrderRepo.FilterOrdersByHistory(purchasedOrders, orderHistory.Id);

            foreach(var o in purchasedOrders)
            {
                var product = ProductRepo.GetById(o.ProductId);
                o.ProductName = product.Name;
            }
            orderHistory.orders = purchasedOrders;

            HistoryDetailsViewModel viewModel = new HistoryDetailsViewModel
            {
                Id = orderHistory.Id,
                CustomerName = fullName,
                LocationAddress = address,
                TimeOrdered = orderHistory.TimeOrdered,
                orders = orderHistory.orders.Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    ProductName = o.ProductName,
                    amountOrdered = (int)o.AmountOrdered
                })
            };
            
            return View(viewModel);
        }
    }
}
