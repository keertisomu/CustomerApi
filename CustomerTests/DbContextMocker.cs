using CustomerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerTests
{
    public static class DbContextMocker
    {
        public static CustomerContext GetCustomerContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new CustomerContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
