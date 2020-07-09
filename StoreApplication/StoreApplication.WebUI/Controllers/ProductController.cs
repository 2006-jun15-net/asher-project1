using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreApplication.Library;
using StoreApplication.Library.Models;
using StoreApplication.WebUI.ViewModels;

namespace StoreApplication.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public IProductRepository ProductRepo { get; }
        public IInventoryRepository InventoryRepo { get; }
        public ILocationRepository LocationRepo { get; }
        public IOrdersRepository OrdersRepo { get; }
        public IOrderHistoryRepository OrderHistoryRepo { get; }

        public ProductController(IProductRepository prepo, IInventoryRepository iRepo, ILocationRepository lRepo, IOrderHistoryRepository hRepo,
          IOrdersRepository oRepo)
        {
            ProductRepo = prepo ?? throw new ArgumentNullException(nameof(prepo));
            InventoryRepo = iRepo ?? throw new ArgumentNullException(nameof(iRepo));
            LocationRepo = lRepo ?? throw new ArgumentNullException(nameof(lRepo));
            OrdersRepo = oRepo ?? throw new ArgumentNullException(nameof(oRepo));
            OrderHistoryRepo = hRepo ?? throw new ArgumentNullException(nameof(hRepo));
        }
            

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            IEnumerable<Product> products = ProductRepo.GetAll();
            IEnumerable<ProductViewModel> viewModels = products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                AmountOrdered = 0
            });

            return View(viewModels);
        }

        // parameter name needs to be productList because since I made a new variable in the View that was a different type than was was strongly typed and because we were passing in this new variable as the argument for this post method, they needed to be the same name so http could identify it
        [HttpPost]
        public ActionResult Create(IEnumerable<ProductViewModel> productList)
        {
            IEnumerable<Product> products = ProductRepo.GetAll();
            IEnumerable<ProductViewModel> viewModels = products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                AmountOrdered = 0
            });

            try
            {
                if(!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "A whole number must be entered in for Amount");
                    return View(viewModels);
                }

                var orderHistory = new OrderHistory();
                List<Order> productOrders = new List<Order>();
                Location location = LocationRepo.GetByID(Int32.Parse(TempData["selectedLocation"].ToString()));
                foreach(var p in productList)
                {
                    if(p.AmountOrdered > 0)
                    {
                        int productId = ProductRepo.FindProduct(p.Name);
                        if(ProductRepo.ExceedMaxAmount(p.AmountOrdered, productId))
                        {
                            ModelState.AddModelError("", "One or more of the products ordered exceeds the max amount that can be purchased in a single order");
                            return View(viewModels);
                        }

                        var product = ProductRepo.GetById(productId);
                        var inventory = InventoryRepo.FindLocationInventory(location, product);
                        if(InventoryRepo.ExceedInventory(p.AmountOrdered, inventory.Id))
                        {
                            ModelState.AddModelError("", "One or more of the products selected exceeds the current stock at this location");
                            return View(viewModels);
                        }

                        var order = new Order()
                        {
                            ProductId = productId,
                            AmountOrdered = p.AmountOrdered
                        };
                        InventoryRepo.UpdateStock(inventory, p.AmountOrdered);
                        productOrders.Add(order);
                    }
                }

                orderHistory.CustomerId = Int32.Parse(TempData["currentCustomerID"].ToString());
                TempData.Keep();
                orderHistory.LocationId = location.Id;
                orderHistory.TimeOrdered = DateTime.Now;
                OrderHistoryRepo.AddOrderHistory(orderHistory);
                OrderHistoryRepo.Save();
                orderHistory.Id = OrderHistoryRepo.GetLatestOrderHistory((int)orderHistory.CustomerId);
                OrdersRepo.AddListOfOrders(productOrders, orderHistory);
                OrdersRepo.Save();

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(productList);
            }
        }
    }
}
