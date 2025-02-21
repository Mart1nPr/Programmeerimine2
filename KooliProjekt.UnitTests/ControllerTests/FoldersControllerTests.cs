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
        private readonly Mock<IFolderService> _folderServiceMock;
        private readonly FoldersController _controller;

        public FoldersControllerTests()
        {
            _folderServiceMock = new Mock<IFolderService>();
            _controller = new FoldersController(_folderServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_view_and_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Folder>
            {
                new Folder { Name = "Name1", Description = "Description1", Creation_date = DateTime.Now },
                new Folder { Name = "Name2", Description = "Description2", Creation_date = DateTime.Now }
            };
            var pagedResult = new PagedResult<Folder>
            {
                Results = data,
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 5,
                RowCount = 2
            };

            // Setup mock service to return the paged result
            _folderServiceMock.Setup(x => x.List(page, 5, It.IsAny<FoldersSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is not null
            var model = result.Model as FoldersIndexModel;
            Assert.NotNull(model);  // Ensure the model is of type FoldersIndexModel
            Assert.Equal(pagedResult, model.Data);  // Assert that the paged result matches the model's data
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_folder_found()
        {
            // Arrange
            int id = 1;
            var folder = new Folder { Id = id, Name = "Folder 1", Description = "Description 1", Creation_date = DateTime.Now };
            _folderServiceMock.Setup(x => x.Get(id)).ReturnsAsync(folder);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(folder, result.Model);  // Assert that the model returned matches the expected folder
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_folder_found()
        {
            // Arrange
            int id = 1;
            var folder = new Folder { Id = id, Name = "Folder 1", Description = "Description 1", Creation_date = DateTime.Now };
            _folderServiceMock.Setup(x => x.Get(id)).ReturnsAsync(folder);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(folder, result.Model);  // Assert that the model returned matches the expected folder
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_folder_found()
        {
            // Arrange
            int id = 1;
            var folder = new Folder { Id = id, Name = "Folder 1", Description = "Description 1", Creation_date = DateTime.Now };
            _folderServiceMock.Setup(x => x.Get(id)).ReturnsAsync(folder);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);  // Ensure the result is a ViewResult
            Assert.Equal(folder, result.Model);  // Assert that the model returned matches the expected folder
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
