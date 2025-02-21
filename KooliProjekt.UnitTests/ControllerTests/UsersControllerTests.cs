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
            Assert.NotNull(result);  // Ensure the result is not null
            var model = result.Model as UsersIndexModel;
            Assert.NotNull(model);  // Ensure the model is of type UsersIndexModel
            Assert.Equal(pagedResult, model.Data);  // Assert that the paged result matches the model's data
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
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(user, result.Model);  // Assert that the model returned matches the expected user
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
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(user, result.Model);  // Assert that the model returned matches the expected user
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
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(user, result.Model);  // Assert that the model returned matches the expected user
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
