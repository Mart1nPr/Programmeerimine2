using KooliProjekt.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KooliProjekt.IntegrationTests.Helpers
{
    public abstract class TestBase : IDisposable
    {
        public WebApplicationFactory<FakeStartup> Factory { get; }

        // Expose ApplicationDbContext through a property
        protected ApplicationDbContext _context => Factory.Services.GetService<ApplicationDbContext>();

        public TestBase()
        {
            Factory = new TestApplicationFactory<FakeStartup>();
        }

        public void Dispose()
        {
            var dbContext = _context;
            dbContext.Database.EnsureDeleted();
        }

        // Add your other helper methods here
    }
}
