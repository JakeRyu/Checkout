using System;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UnitTests.Common
{
    public class CheckoutDbContextFactory
    {
        public static CheckoutDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CheckoutDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
            var context = new CheckoutDbContext(options);

            context.Database.EnsureCreated();
            
            return context;
        }

        public static void Destroy(CheckoutDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}