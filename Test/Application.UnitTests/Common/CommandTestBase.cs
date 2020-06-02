using System;
using Persistence;

namespace Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly CheckoutDbContext Context;

        public CommandTestBase()
        {
            Context = CheckoutDbContextFactory.Create();
        }
        
        public void Dispose()
        {
            CheckoutDbContextFactory.Destroy(Context);
        }
    }
}