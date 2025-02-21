using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using KooliProjekt.Models;
using KooliProjekt.Search;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class PicturesControllerTests
    {
        private readonly Mock<IPictureService> _PictureServiceMock;
        private readonly PicturesController _controller;

        public PicturesControllerTests()
        {
            _PictureServiceMock = new Mock<IPictureService>();
            _controller = new PicturesController(_PictureServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Picture>
            {
                new Picture { ImageLink = "Img1", Name = "Name1", Context = "Context1", Creation_date = DateTime.Now, Latitude = 38, Longitude = 58 },
                new Picture { ImageLink = "Img2", Name = "Name2", Context = "Context2", Creation_date = DateTime.Now, Latitude = 29, Longitude = 59 }
            };
            var pagedResult = new PagedResult<Picture> { Results = data };

            // Setup mock service to return the paged result
            _PictureServiceMock.Setup(x => x.List(page, 5, It.IsAny<PicturesSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as PicturesIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
        }

    }
}
