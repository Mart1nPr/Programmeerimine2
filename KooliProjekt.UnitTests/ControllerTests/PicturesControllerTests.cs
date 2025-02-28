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
            Assert.NotNull(result);
            var model = result.Model as PicturesIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
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
            Assert.NotNull(result);
            Assert.Equal(picture, result.Model);
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
            Assert.NotNull(result);
            Assert.Equal(picture, result.Model);
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
            Assert.NotNull(result);
            Assert.Equal(picture, result.Model);
        }

        [Fact]
        public void Create_should_return_view()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_should_return_view_when_model_is_invalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.Create(new Picture()) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(!_controller.ModelState.IsValid);
        }

        [Fact]
        public async Task Create_should_redirect_to_index_when_model_is_valid()
        {
            // Arrange
            var newPicture = new Picture { Name = "New Picture", ImageLink = "Link", Context = "New Context", Creation_date = DateTime.Now, Latitude = 38, Longitude = 58 };
            _pictureServiceMock.Setup(x => x.Create(It.IsAny<Picture>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(newPicture) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_not_found_when_picture_does_not_exist()
        {
            // Arrange
            int id = 999;
            _pictureServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Picture)null);

            // Act
            var result = await _controller.Edit(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_should_redirect_to_index_when_picture_deleted()
        {
            // Arrange
            int id = 1;
            var picture = new Picture { Id = id, Name = "Picture 1", ImageLink = "Link1", Context = "Context1", Creation_date = DateTime.Now, Latitude = 38, Longitude = 58 };
            _pictureServiceMock.Setup(x => x.Delete(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Delete_should_return_not_found_when_picture_not_found()
        {
            // Arrange
            int id = 999;
            _pictureServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Picture)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
