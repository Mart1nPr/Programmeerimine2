using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class FolderServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IFolderRepository> _repositoryMock;
        private readonly FolderService _folderService;

        public FolderServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IFolderRepository>();
            _folderService = new FolderService(_uowMock.Object);

            _uowMock.SetupGet(u => u.FolderRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_Should_Return_List_Of_Folders()
        {
            // Arrange
            var results = new List<Folder>
            {
                new Folder { Id = 1, Name = "Folder 1", Description = "Desc 1" },
                new Folder { Id = 2, Name = "Folder 2", Description = "Desc 2" }
            };
            var pagedResult = new PagedResult<Folder> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _folderService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_Should_Return_Folder()
        {
            // Arrange
            var folder = new Folder { Id = 1, Name = "Folder 1", Description = "Desc 1" };
            _repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(folder);

            // Act
            var result = await _folderService.Get(1);

            // Assert
            Assert.Equal(folder, result);
        }

        [Fact]
        public async Task Save_Should_Call_Repository_Save()
        {
            // Arrange
            var folder = new Folder { Id = 1, Name = "Folder 1", Description = "Desc 1" };

            // Act
            await _folderService.Save(folder);

            // Assert
            _repositoryMock.Verify(r => r.Save(folder), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            int id = 1;

            // Act
            await _folderService.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}
