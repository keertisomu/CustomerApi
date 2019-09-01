using CustomerApi.Controllers;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CustomerTests
{
    public class CustomerTests
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, 2+2);
        }

        [Fact]
        public async Task TestGetCustomersAsync()
        {

            // Arrange
            var dbContext = DbContextMocker.GetCustomerContext(nameof(TestGetCustomersAsync));
            var controller = new CustomerController(dbContext);

            // Act
            var response = await controller.GetCustomers();
            
            //dispose dbcntext
            dbContext.Dispose();

            // Assert
            Assert.NotNull(response.Value);
        }


        [Fact]
        public async Task TestGetCustomersAsyncById()
        {

            // Arrange
            var dbContext = DbContextMocker.GetCustomerContext(nameof(TestGetCustomersAsync));
            var controller = new CustomerController(dbContext);
            var customerId = 3;

            // Act
            var response = await controller.GetCustomer(customerId);
            var customerObj = response.Value;

            //dispose dbcntext
            dbContext.Dispose();

            // Assert
            Assert.NotNull(customerObj);
            Assert.Same(customerObj.FirstName , "John");
            Assert.Same(customerObj.LastName, "Doe");

        }

        [Fact]
        public async Task TestPostCustomerAsync()
        {

            // Arrange
            var dbContext = DbContextMocker.GetCustomerContext(nameof(TestGetCustomersAsync));
            var controller = new CustomerController(dbContext);
            var customerId = 3;

            // Act
            var response = await controller.GetCustomer(customerId);
            var customerObj = response.Value;

            //dispose dbcntext
            dbContext.Dispose();

            // Assert
            Assert.NotNull(customerObj);
            Assert.Same(customerObj.FirstName, "John");
            Assert.Same(customerObj.LastName, "Doe");

        }


    }


}
