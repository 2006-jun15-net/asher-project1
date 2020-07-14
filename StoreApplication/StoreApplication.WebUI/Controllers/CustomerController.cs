using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Serilog;
using StoreApplication.Library;
using StoreApplication.Library.Models;
using StoreApplication.WebUI.ViewModels;

namespace StoreApplication.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository Repo { get; }
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerRepository repo, ILogger<CustomerController> logger)
        {
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }
            
            

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn([Bind("FirstName, LastName, UserName")] CustomerViewModel viewModel)
        {
            try
            {
                _logger.LogInformation("User selected to sign in to their account");
                if(ModelState.IsValid)
                {
                    _logger.LogInformation("User submitted a correct form");
                    var customer = Repo.findCustomer(viewModel.FirstName, viewModel.LastName, viewModel.UserName);
                    if(customer != null)
                    {
                        TempData["currentCustomerID"] = customer.Id;
                        TempData["currentCustomer"] = customer.FirstName + " " + customer.LastName;
                        _logger.LogInformation("User has successfully logged into their account");
                        return RedirectToAction("Index", "Home");
                    }
                    
                    _logger.LogError("User entered in an invalid credential");
                    viewModel.ErrorMessage = "One or more credentials were invalid";
                    return View(viewModel);
                }

                _logger.LogError("User submitted an invalid form");
                return View(viewModel);
            }
            catch(ArgumentException ex)
            {
                _logger.LogError(ex, "User entered in an invalid credential");
                return View();
            }
        }


        public ActionResult Create()
        {
            _logger.LogInformation("User selected to create a new account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FirstName, LastName, UserName")] CustomerViewModel viewModel)
        {
            try
            {
                _logger.LogInformation("User has submitted their information to create a new account");
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("User submitted a correct form");
                    var customer = new Customer
                    {
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        UserName = viewModel.UserName
                    };
                    
                    if(!Repo.DoesUsernameExist(customer))
                    {
                        Repo.AddCustomer(customer);
                        Repo.Save();

                        customer = Repo.findCustomer(customer.FirstName, customer.LastName, customer.UserName);
                        TempData["currentCustomerID"] = customer.Id;
                        TempData["currentCustomer"] = customer.FirstName + " " + customer.LastName;
                        _logger.LogInformation("User successfully created a new account");
                        return RedirectToAction("Index", "Home");
                    }

                    _logger.LogError("User entered in an invalid credential");
                    ModelState.AddModelError("UserName", "UserName already exists. Please create another");
                    return View(viewModel);
                }

                _logger.LogError("User submitted an invalid form");
                return View(viewModel);
            }
            catch(ArgumentException ex)
            {
                _logger.LogError(ex, "User entered in an invalid credential");
                return View(viewModel);
            }
        }
    }
}
