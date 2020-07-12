using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StoreApplication.Library;
using StoreApplication.Library.Models;
using StoreApplication.WebUI.ViewModels;

namespace StoreApplication.WebUI.Controllers
{
    public class LocationController : Controller
    {
        public ILocationRepository Repo { get; }

        public LocationController(ILocationRepository repo) =>
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public IActionResult Index()
        {
            return View();
        }


        public ActionResult SelectLocation()
        {
            var customerId = TempData["currentCustomerID"];
            TempData.Keep();
            if (customerId == null)
            {
                return RedirectToAction("SignIn", "Customer");
            }

            IEnumerable<Location> locations = Repo.GetAll();
            IEnumerable<LocationViewModel> viewModels = locations.Select(x => new LocationViewModel
            {
                Id = x.Id,
                Address = x.Address,
                City = x.City,
                State = x.State
            });

            return View(viewModels);
        }

        
        public ActionResult Select(int id)
        {
            var location = Repo.GetByID(id);
            TempData["selectedLocation"] = location.Id;
            return RedirectToAction("Create", "Product");
        }

        public ActionResult ViewHistories(int id)
        {
            var location = Repo.GetByID(id);
            TempData["selectedLocation"] = location.Id;
            return RedirectToAction("LocationHistories", "OrderHistory");
        }
    }
}
