using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OrderHistoryController> _logger;
        public OrderHistoryController(IOrderHistoryRepository hRepo, IOrdersRepository oRepo, 
            IProductRepository pRepo, ILogger<OrderHistoryController> logger)
        {
            OrderHistoryRepo = hRepo ?? throw new ArgumentException(nameof(hRepo));
            OrderRepo = oRepo ?? throw new ArgumentException(nameof(oRepo));
            ProductRepo = pRepo ?? throw new ArgumentException(nameof(pRepo));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        public IActionResult Index()
        {
            _logger.LogInformation("User directed to OrderHistory Index page");
            return View();
        }

        public ActionResult LocationHistories()
        {
            _logger.LogInformation("User is viewing location order histories");
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
            _logger.LogInformation("User is viewing their order histories");
            var customerId = TempData["currentCustomerID"];
            TempData.Keep();
            if (customerId == null)
            {
                return RedirectToAction("SignIn", "Customer");
            }

            IEnumerable<OrderHistory> customerHistories = OrderHistoryRepo.GetAllCustomerOrders((int)customerId);
            IEnumerable<OrderHistoryViewModel> viewModels = customerHistories.Select(x => new OrderHistoryViewModel
            {
                Id = x.Id,
                TimeOrdered = x.TimeOrdered,
            });

            return View(viewModels);
        }

        public ActionResult Details(int id)
        {
            _logger.LogInformation("User is viewing a specific order history");
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
