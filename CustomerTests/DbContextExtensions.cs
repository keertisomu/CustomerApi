using CustomerApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerTests
{
    public static class DbContextExtensions
    {
        public static void Seed(this CustomerContext dbContext)
        {
            dbContext.Customers.Add(new Customer
            {
                Id = 2,
                FirstName = "Keerti",
                LastName = "Somasundaram",
                DateOfBirth = Convert.ToDateTime("1984-01-24T00: 00:00")
            });

            dbContext.Customers.Add(new Customer
            {
                Id = 3,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = Convert.ToDateTime("1965-06-10T00: 00:00")
            });


            dbContext.Customers.Add(new Customer
            {
                Id = 4,
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = Convert.ToDateTime("2001-04-15T00: 00:00")
            });
        }
    }
}
