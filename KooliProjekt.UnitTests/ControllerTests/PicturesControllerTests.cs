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
        private readonly Mock<IPictureService> _pictureServiceMock;
        private readonly PicturesController _controller;

        public PicturesControllerTests()
        {
            _pictureServiceMock = new Mock<IPictureService>();
            _controller = new PicturesController(_pictureServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_view_and_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Picture>
            {
                new Picture { ImageLink = "Img1", Name = "Name1", Context = "Context1", Creation_date = DateTime.Now, Latitude = 38, Longitude = 58 },
                new Picture { ImageLink = "Img2", Name = "Name2", Context = "Context2", Creation_date = DateTime.Now, Latitude = 29, Longitude = 59 }
            };
            var pagedResult = new PagedResult<Picture>
            {
                Results = data,
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 5,
                RowCount = 2
            };
            _pictureServiceMock.Setup(x => x.List(page, 5, It.IsAny<PicturesSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is not null
            var model = result.Model as PicturesIndexModel;
            Assert.NotNull(model);  // Ensure the model is of type PicturesIndexModel
            Assert.Equal(pagedResult, model.Data);  // Assert that the paged result matches the model's data
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_picture_found()
        {
            // Arrange
            int id = 1;
            var picture = new Picture { Id = id, Name = "Picture 1", ImageLink = "Link1", Context = "Context1", Creation_date = DateTime.Now, Latitude = 38, Longitude = 58 };
            _pictureServiceMock.Setup(x => x.Get(id)).ReturnsAsync(picture);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(picture, result.Model);  // Assert that the model returned matches the expected picture
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_picture_found()
        {
            // Arrange
            int id = 1;
            var picture = new Picture { Id = id, Name = "Picture 1", ImageLink = "Link1", Context = "Context1", Creation_date = DateTime.Now, Latitude = 38, Longitude = 58 };
            _pictureServiceMock.Setup(x => x.Get(id)).ReturnsAsync(picture);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(picture, result.Model);  // Assert that the model returned matches the expected picture
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_picture_found()
        {
            // Arrange
            int id = 1;
            var picture = new Picture { Id = id, Name = "Picture 1", ImageLink = "Link1", Context = "Context1", Creation_date = DateTime.Now, Latitude = 38, Longitude = 58 };
            _pictureServiceMock.Setup(x => x.Get(id)).ReturnsAsync(picture);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(picture, result.Model);  // Assert that the model returned matches the expected picture
        }

        [Fact]
        public void Create_should_return_view()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
        }
    }
}
