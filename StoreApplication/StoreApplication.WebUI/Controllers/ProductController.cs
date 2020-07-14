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
    public class ProductController : Controller
    {
        private IProductRepository ProductRepo { get; }
        private IInventoryRepository InventoryRepo { get; }
        private ILocationRepository LocationRepo { get; }
        private IOrdersRepository OrdersRepo { get; }
        private IOrderHistoryRepository OrderHistoryRepo { get; }

        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository prepo, IInventoryRepository iRepo, ILocationRepository lRepo, IOrderHistoryRepository hRepo,
          IOrdersRepository oRepo,
          ILogger<ProductController> logger)
        {
            ProductRepo = prepo ?? throw new ArgumentNullException(nameof(prepo));
            InventoryRepo = iRepo ?? throw new ArgumentNullException(nameof(iRepo));
            LocationRepo = lRepo ?? throw new ArgumentNullException(nameof(lRepo));
            OrdersRepo = oRepo ?? throw new ArgumentNullException(nameof(oRepo));
            OrderHistoryRepo = hRepo ?? throw new ArgumentNullException(nameof(hRepo));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }
            

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            _logger.LogInformation("User is now selecting products to add");
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(IEnumerable<ProductViewModel> productList)
        {
            _logger.LogInformation("User has submitted their products to purchase");
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
                    _logger.LogError("User submitted invalid form");
                    ModelState.AddModelError("", "You must enter a positive whole number");
                    return View(viewModels);
                }

                _logger.LogInformation("User submitted valid form");
                var orderHistory = new OrderHistory();
                List<Order> productOrders = new List<Order>();
                Location location = LocationRepo.GetByID(Int32.Parse(TempData["selectedLocation"].ToString()));
                bool productOrdered = false;

                foreach(var p in productList)
                {
                    if(p.AmountOrdered > 0)
                    {
                        int productId = ProductRepo.FindProduct(p.Name);
                        if(ProductRepo.ExceedMaxAmount(p.AmountOrdered, productId))
                        {
                            _logger.LogError("User entered value that exceeded MaxAmount");
                            ModelState.AddModelError("", "One or more of the products ordered exceeds the max amount that can be purchased in a single order");
                            return View(viewModels);
                        }

                        var product = ProductRepo.GetById(productId);
                        var inventory = InventoryRepo.FindLocationInventory(location, product);
                        if(InventoryRepo.ExceedInventory(p.AmountOrdered, inventory.Id))
                        {
                            _logger.LogError("User entered value that exceeds inventory");
                            ModelState.AddModelError("", "One or more of the products selected exceeds the current stock at this location");
                            return View(viewModels);
                        }

                        productOrdered = true;
                        var order = new Order()
                        {
                            ProductId = productId,
                            AmountOrdered = p.AmountOrdered
                        };
                        InventoryRepo.UpdateStock(inventory, p.AmountOrdered);
                        productOrders.Add(order);
                        _logger.LogInformation("Products successfully added to order");
                    }
                }

                if(productOrdered)
                {
                    orderHistory.CustomerId = Int32.Parse(TempData["currentCustomerID"].ToString());
                    TempData.Keep();
                    orderHistory.LocationId = location.Id;
                    orderHistory.TimeOrdered = DateTime.Now;
                    OrderHistoryRepo.AddOrderHistory(orderHistory);
                    OrderHistoryRepo.Save();
                    orderHistory.Id = OrderHistoryRepo.GetLatestOrderHistory((int)orderHistory.CustomerId);
                    OrdersRepo.AddListOfOrders(productOrders, orderHistory);
                    OrdersRepo.Save();

                    _logger.LogInformation("User successfully created an order");
                    return RedirectToAction("Index", "Home");
                }

                return View(viewModels);
            }
            catch(ArgumentException ex)
            {
                _logger.LogError(ex, "User entered invalid argument");
                return View(productList);
            }
        }
    }
}
