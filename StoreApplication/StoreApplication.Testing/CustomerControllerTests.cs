using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StoreApplication.Library;
using StoreApplication.Library.Models;
using StoreApplication.WebUI.Controllers;
using StoreApplication.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreApplication.Testing
{
    /*
    public class CustomerControllerTests
    {

        [Fact]
        public void SignIn_Successful()
        {
            //Arrange
            var customerMockRepo = new Mock<ICustomerRepository>();
            var loggerMock = new Mock<ILogger<CustomerController>>();

            customerMockRepo.Setup(repo => repo
            .findCustomer("Asher", "Williams", "Trasher571"))
                .Returns(new Library.Models.Customer()
                {
                    Id = 1,
                    FirstName = "Asher",
                    LastName = "Williams",
                    UserName = "Trasher571"
                });

            var controller = new CustomerController(customerMockRepo.Object, loggerMock.Object);

            // Act
            var result = (ViewResult)controller.SignIn(new CustomerViewModel());

            //Assert
            Assert.IsType<CustomerViewModel>(result.ViewData.Model);
        }

        [Fact]
        public void CreateNewCustomer_Successful()
        {
            //Arrange
            var customerMockRepo = new Mock<ICustomerRepository>();
            var loggerMock = new Mock<ILogger<CustomerController>>();
            var C = new CustomerViewModel()
            {
                ErrorMessage = "",
                FirstName = "test",
                LastName = "Test",
                UserName = "Testing"
            };

            customerMockRepo.Setup(repo => repo.AddCustomer(It.IsAny<Customer>())).Verifiable();
            var controller = new CustomerController(customerMockRepo.Object, loggerMock.Object);

            // Act
            var result = (ViewResult)controller.Create(C);

            // Assert
            Assert.IsType<CustomerViewModel>(result.ViewData.Model);
        }
    }
    */
}
