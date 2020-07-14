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
    public class LocationController : Controller
    {
        public ILocationRepository Repo { get; }
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILocationRepository repo, ILogger<LocationController> logger)
        {
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        public IActionResult Index()
        {
            return View();
        }


        public ActionResult SelectLocation()
        {
            _logger.LogInformation("User directed to location selection screen");
            var customerId = TempData["currentCustomerID"];
            TempData.Keep();
            if (customerId == null)
            {
                _logger.LogInformation("User not signed in. Redirecting to Sign-In page");
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
            _logger.LogInformation("User directed to product selection screen");
            var location = Repo.GetByID(id);
            TempData["selectedLocation"] = location.Id;
            return RedirectToAction("Create", "Product");
        }

        public ActionResult ViewHistories(int id)
        {
            _logger.LogInformation("User directed to location histories screen");
            var location = Repo.GetByID(id);
            TempData["selectedLocation"] = location.Id;
            return RedirectToAction("LocationHistories", "OrderHistory");
        }
    }
}
