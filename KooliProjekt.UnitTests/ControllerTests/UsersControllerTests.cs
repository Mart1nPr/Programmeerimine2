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
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UsersController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_view_and_data()
        {
            // Arrange
            int page = 1;
            var data = new List<User>
            {
                new User { Id = 1, Name = "Name1", Password = "Password1", Registration_Time = DateTime.Now },
                new User { Id = 2, Name = "Name2", Password = "Password2", Registration_Time = DateTime.Now }
            };
            var pagedResult = new PagedResult<User>
            {
                Results = data,
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 5,
                RowCount = 2
            };
            _userServiceMock.Setup(x => x.List(page, It.IsAny<int>(), It.IsAny<UsersSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page, null) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as UsersIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_user_found()
        {
            // Arrange
            int id = 1;
            var user = new User { Id = id, Name = "User 1", Password = "Password1", Registration_Time = DateTime.Now };
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_user_found()
        {
            // Arrange
            int id = 1;
            var user = new User { Id = id, Name = "User 1", Password = "Password1", Registration_Time = DateTime.Now };
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_user_found()
        {
            // Arrange
            int id = 1;
            var user = new User { Id = id, Name = "User 1", Password = "Password1", Registration_Time = DateTime.Now };
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync(user);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
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
            var result = await _controller.Create(new User()) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(!_controller.ModelState.IsValid);
        }

        [Fact]
        public async Task Create_should_redirect_to_index_when_model_is_valid()
        {
            // Arrange
            var newUser = new User { Name = "New User", Password = "Password", Registration_Time = DateTime.Now };
            _userServiceMock.Setup(x => x.Create(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(newUser) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_not_found_when_user_does_not_exist()
        {
            // Arrange
            int id = 999;
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Edit(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_should_redirect_to_index_when_user_deleted()
        {
            // Arrange
            int id = 1;
            var user = new User { Id = id, Name = "User 1", Password = "Password1", Registration_Time = DateTime.Now };
            _userServiceMock.Setup(x => x.Delete(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Delete_should_return_not_found_when_user_not_found()
        {
            // Arrange
            int id = 999;
            _userServiceMock.Setup(x => x.Get(id)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
