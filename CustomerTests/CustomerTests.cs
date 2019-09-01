using CustomerApi.Controllers;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CustomerTests
{
    public class CustomerTests
    {
       
        [Fact]
        public async Task TestGetCustomersAsync()
        {

            // Arrange
            var dbContext = DbContextMocker.GetCustomerContext(nameof(TestGetCustomersAsync));
            var controller = new CustomerController(dbContext);

            // Act
            var response = await controller.GetCustomersAsync();
            
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
            var response = await controller.GetCustomerAsync(customerId);
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
            var customer = new Customer
            {
                Id = 5,
                FirstName = "Nelson",
                LastName = "Mandela",
                DateOfBirth = Convert.ToDateTime("1918-07-18T00: 00:00")
            };

            // Act

            var response = await controller.AddCustomerAsync(customer);
            CreatedAtActionResult result = response.Result as CreatedAtActionResult;
            var statusCode = result.StatusCode;
            var customerVal = result.Value as Customer;

            //dispose dbcntext
            dbContext.Dispose();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(statusCode);
            Assert.Equal(201, statusCode);
            Assert.Same(customerVal.FirstName, "Nelson");
            Assert.Same(customerVal.LastName, "Mandela");
            Assert.Equal(customerVal.DateOfBirth, Convert.ToDateTime("7/18/1918 12:00:00 AM"));
        }

        [Fact]
        public async Task TestEditCustomerAsync()
        {

            // Arrange
            var dbContext = DbContextMocker.GetCustomerContext(nameof(TestGetCustomersAsync));
            var controller = new CustomerController(dbContext);
            var customerId = 4;

            // Act

            var response = await controller.GetCustomerAsync(customerId);
            var customerObj = response.Value as Customer;

            //edit customer values
            customerObj.LastName = "Smith";
            customerObj.DateOfBirth = Convert.ToDateTime("1986-11-21T00: 00:00");

            var editResponse = await controller.UpdateCustomerAsync(customerId, customerObj) as NoContentResult; 

            //dispose dbcntext
            dbContext.Dispose();

            // Assert
            Assert.NotNull(editResponse);
            Assert.Equal(204, editResponse.StatusCode);
        }

        [Fact]
        public async Task TestSearchCustomerAsync()
        {

            // Arrange
            var dbContext = DbContextMocker.GetCustomerContext(nameof(TestGetCustomersAsync));
            var controller = new CustomerController(dbContext);
            
            // Act
            var response = await controller.FindCustomersAsync("Jane" , "Somasundaram");
            var customers = response.Value as List<Customer>;

            //dispose dbcntext
            dbContext.Dispose();

            // Assert
            Assert.NotNull(customers);
            var customerJane = customers[0];
            Assert.Same(customerJane.FirstName, "Jane");
        }

    }


}
