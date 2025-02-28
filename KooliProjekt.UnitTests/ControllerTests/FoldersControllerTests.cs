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
            _folderServiceMock.Setup(x => x.List(page, 5, It.IsAny<FoldersSearch>())).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as FoldersIndexModel;
            Assert.NotNull(model);
            Assert.Equal(pagedResult, model.Data);
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
            Assert.NotNull(result);
            Assert.Equal(folder, result.Model);
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
            Assert.NotNull(result);
            Assert.Equal(folder, result.Model);
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
            Assert.NotNull(result);
            Assert.Equal(folder, result.Model);
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
            var result = await _controller.Create(new Folder()) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(!_controller.ModelState.IsValid);
        }

        [Fact]
        public async Task Create_should_redirect_to_index_when_model_is_valid()
        {
            // Arrange
            var newFolder = new Folder { Name = "New Folder", Description = "New Description", Creation_date = DateTime.Now };
            _folderServiceMock.Setup(x => x.Create(It.IsAny<Folder>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(newFolder) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_not_found_when_folder_does_not_exist()
        {
            // Arrange
            int id = 999;
            _folderServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Folder)null);

            // Act
            var result = await _controller.Edit(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_should_redirect_to_index_when_folder_deleted()
        {
            // Arrange
            int id = 1;
            var folder = new Folder { Id = id, Name = "Folder 1", Description = "Description 1", Creation_date = DateTime.Now };
            _folderServiceMock.Setup(x => x.Delete(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Delete_should_return_not_found_when_folder_not_found()
        {
            // Arrange
            int id = 999;
            _folderServiceMock.Setup(x => x.Get(id)).ReturnsAsync((Folder)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
