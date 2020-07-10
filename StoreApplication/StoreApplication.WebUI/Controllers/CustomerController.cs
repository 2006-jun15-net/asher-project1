using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreApplication.Library;
using StoreApplication.Library.Models;
using StoreApplication.WebUI.ViewModels;

namespace StoreApplication.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        public ICustomerRepository Repo { get; }

        public CustomerController(ICustomerRepository repo) =>
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn([Bind("FirstName, LastName, UserName")] CustomerViewModel viewModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var customer = Repo.findCustomer(viewModel.FirstName, viewModel.LastName, viewModel.UserName);
                    if(customer != null)
                    {
                        TempData["currentCustomerID"] = customer.Id;
                        TempData["currentCustomer"] = customer.FirstName + " " + customer.LastName;
                        return RedirectToAction("Index", "Home");
                    }
                    return View();
                }

                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FirstName, LastName, UserName")] CustomerViewModel viewModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var customer = new Customer
                    {
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        UserName = viewModel.UserName
                    };
                    customer = Repo.findCustomer(customer.FirstName, customer.LastName, customer.UserName);
                    if(customer != null)
                    {
                        Repo.AddCustomer(customer);
                        Repo.Save();

                        customer = Repo.findCustomer(customer.FirstName, customer.LastName, customer.UserName);
                        TempData["currentCustomerID"] = customer.Id;
                        TempData["currentCustomer"] = customer.FirstName + " " + customer.LastName;
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }
    }
}
