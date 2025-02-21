using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Search;
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
                new Folder { Name = "Name1", Description = "Description1", Creation_date = DateTime.Now },
                new Folder { Name = "Name2", Description = "Description2", Creation_date = DateTime.Now },
            };
            var pagedResult = new PagedResult<Folder> { Results = data };  // The expected paged result

            // Setup mock service to return the paged result
            _FolderServiceMock.Setup(x => x.List(page, 5, It.IsAny<FoldersSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;  // Call the Index method

            // Assert
            Assert.NotNull(result); 
            var model = result.Model as FoldersIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
        }

    }
}
