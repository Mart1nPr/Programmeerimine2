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
                new Picture {ImageLink = "Img2", Name = "Name1", Context = "Context", Creation_date = DateTime.Now, Latitude=38, Longitude = 58},
                new Picture {ImageLink = "Img2", Name = "Name2", Context = "Context2", Creation_date = DateTime.Now, Latitude=29, Longitude = 59},
            };
            var pagedResult = new PagedResult<Picture> { Results = data };
            _PictureServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }
    }
}
