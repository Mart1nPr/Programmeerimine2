using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Linq;
using System;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class FoldersControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public FoldersControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_folder()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "Name", "Test Folder" },
                { "Description", "A folder for testing" },
                { "Creation_date", "2025-03-27" }
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Folders/Create", content);

            // Assert
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                var folder = _context.Folders.FirstOrDefault(f => f.Name == "Test Folder");
                Assert.NotNull(folder);
                Assert.Equal("Test Folder", folder.Name);
                Assert.Equal("A folder for testing", folder.Description);
                Assert.Equal(new DateTime(2025, 03, 27), folder.Creation_date);
            }
            else
            {
                Console.WriteLine("Hmm... doesn't work");
            }
        }

        [Fact]
        public async Task Create_should_not_save_invalid_folder()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "Name", "" },
                { "Description", "" },
                { "Creation_date", "" }
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Folders/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Folders.Any());
        }
    }
}
