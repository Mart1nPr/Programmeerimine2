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
        private readonly Mock<IFolderService> _FolderServiceMock;
        private readonly FoldersController _controller;

        public FoldersControllerTests()
        {
            _FolderServiceMock = new Mock<IFolderService>();
            _controller = new FoldersController(_FolderServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Folder>
            {
                new Folder { Name = "Test 1", Description = "Test folder", Creation_date = DateTime.Now },
                new Folder { Name = "Test 2", Description = "Test folder 2", Creation_date = DateTime.Now },
            };
            var pagedResult = new PagedResult<Folder> { Results = data };
            _FolderServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);
        }
    }
}
