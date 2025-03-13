using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class FolderServiceTests : ServiceTestBase
    {
        private readonly IFolderService _folderService;

        public FolderServiceTests()
        {
            _folderService = new FolderService(DbContext);
        }

        [Fact]
        public async Task Create_ShouldAddFolder()
        {
            // Arrange
            var folder = new Folder
            {
                Name = "Test Folder",
                Description = "This is a test folder."
            };

            // Act
            await _folderService.Create(folder);

            // Assert
            var createdFolder = await DbContext.Folders.FindAsync(folder.Id);
            Assert.NotNull(createdFolder);
            Assert.Equal("Test Folder", createdFolder.Name);
            Assert.Equal("This is a test folder.", createdFolder.Description);
        }

        [Fact]
        public async Task List_ShouldReturnPagedFolders()
        {
            // Arrange
            var folder1 = new Folder { Name = "Folder 1", Description = "Description 1" };
            var folder2 = new Folder { Name = "Folder 2", Description = "Description 2" };
            await _folderService.Create(folder1);
            await _folderService.Create(folder2);

            var search = new FoldersSearch { Keyword = "Folder" };

            // Act
            var result = await _folderService.List(1, 10, search);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task Get_ShouldReturnFolderById()
        {
            // Arrange
            var folder = new Folder
            {
                Name = "Test Folder",
                Description = "This is a test folder."
            };
            await _folderService.Create(folder);

            // Act
            var fetchedFolder = await _folderService.Get(folder.Id);

            // Assert
            Assert.NotNull(fetchedFolder);
            Assert.Equal("Test Folder", fetchedFolder.Name);
            Assert.Equal("This is a test folder.", fetchedFolder.Description);
        }

        [Fact]
        public async Task Save_ShouldUpdateFolder()
        {
            // Arrange
            var folder = new Folder
            {
                Name = "Test Folder",
                Description = "This is a test folder."
            };
            await _folderService.Create(folder);

            // Act
            folder.Name = "Updated Folder Name";
            folder.Description = "Updated Description";
            await _folderService.Save(folder);

            // Assert
            var updatedFolder = await _folderService.Get(folder.Id);
            Assert.NotNull(updatedFolder);
            Assert.Equal("Updated Folder Name", updatedFolder.Name);
            Assert.Equal("Updated Description", updatedFolder.Description);
        }

        [Fact]
        public async Task Delete_ShouldRemoveFolder()
        {
            // Arrange
            var folder = new Folder
            {
                Name = "Test Folder",
                Description = "This is a test folder."
            };
            await _folderService.Create(folder);

            // Act
            await _folderService.Delete(folder.Id);

            // Assert
            var deletedFolder = await DbContext.Folders.FindAsync(folder.Id);
            Assert.Null(deletedFolder);
        }
    }
}
