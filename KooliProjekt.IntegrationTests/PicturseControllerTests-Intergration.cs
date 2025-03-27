using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Linq;
using System;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class PicturesControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public PicturesControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Create_should_save_new_picture()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "ImageLink", "http://example.com/image.jpg" },
                { "Name", "Test Picture" },
                { "Context", "A picture taken in the park" },
                { "Creation_date", "2025-03-27" },
                { "Latitude", "40" },
                { "Longitude", "-74" }
            };

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Pictures/Create", content);

            // Assert
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                var picture = _context.Pictures.FirstOrDefault(p => p.Name == "Test Picture");
                Assert.NotNull(picture);
                Assert.Equal("http://example.com/image.jpg", picture.ImageLink);
                Assert.Equal("Test Picture", picture.Name);
                Assert.Equal("A picture taken in the park", picture.Context);
                Assert.Equal(new DateTime(2025, 03, 27), picture.Creation_date);
                Assert.Equal(40, picture.Latitude);
                Assert.Equal(-74, picture.Longitude);
            }
            else
            {
                Assert.Fail("Hmm... doesn't work");
            }
        }

        [Fact]
        public async Task Create_should_not_save_invalid_picture()
        {
            // Arrange
            var formValues = new Dictionary<string, string>
            {
                { "ImageLink", "" },
                { "Name", "" },
                { "Context", "" },
                { "Creation_date", "" },
                { "Latitude", "" },
                { "Longitude", "" }
            };

            // Convert
            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Pictures/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(_context.Pictures.Any());
        }
    }
}
