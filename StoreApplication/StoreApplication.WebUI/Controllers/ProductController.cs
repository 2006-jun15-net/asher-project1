using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreApplication.Library;
using StoreApplication.Library.Models;
using StoreApplication.WebUI.ViewModels;

namespace StoreApplication.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public IProductRepository Repo { get; }

        public ProductController(IProductRepository repo) =>
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            IEnumerable<Product> products = Repo.GetAll();
            IEnumerable<ProductViewModel> viewModels = products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                AmountOrdered = 0
            });

            return View(viewModels);
        }

        [HttpPost]
        public ActionResult Create([Bind("AmountOrdered")] IEnumerable<ProductViewModel> productsOrdered)
        {
            try
            {
                if(productsOrdered.Any(p => TryValidateModel(p) == false))
                {
                    IEnumerable<Product> products = Repo.GetAll();
                    IEnumerable<ProductViewModel> viewModels = products.Select(x => new ProductViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        AmountOrdered = 0
                    });
                    return View(viewModels);
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(productsOrdered);
            }
        }
    }
}
