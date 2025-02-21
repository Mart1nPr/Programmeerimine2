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
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<User>
            {
                new User { Id = 1, Name = "Name1", Password = "Password1", Registration_Time = DateTime.Now },
                new User { Id = 2, Name = "Name2", Password = "Password2", Registration_Time = DateTime.Now }
            };
            var pagedResult = new PagedResult<User> { Results = data };

            // Setup mock service to return the paged result
            _userServiceMock.Setup(x => x.List(page, 5, It.IsAny<UsersSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);  
            var model = result.Model as UsersIndexModel;
            Assert.NotNull(model);  
            Assert.Equal(pagedResult, model.Data);
        }
    }
}
