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
    public class FoldersControllerTests
    {
        private readonly Mock<IUserService> _FolderServiceMock;
        private readonly UsersController _controller;

        public FoldersControllerTests()
        {
            _FolderServiceMock = new Mock<IUserService>();
            _controller = new UsersController(_FolderServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Folders>
            {
                new Folders { Name = "Test 1", Description = "Test folder", Creation_date = DateTime.Now },
                new Folders { Name = "Test 2", Description = "Test folder 2", Creation_date = DateTime.Now },
            };
            var pagedResult = new PagedResult<Users> { Results = data };
            _FolderServiceMock.Setup(x => x.List(page, It.IsAny<int>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }
    }
}
