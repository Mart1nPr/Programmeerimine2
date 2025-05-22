using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class PictureServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IPictureRepository> _repositoryMock;
        private readonly PictureService _pictureService;

        public PictureServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IPictureRepository>();
            _pictureService = new PictureService(_uowMock.Object);

            _uowMock.SetupGet(u => u.PictureRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_Should_Return_List_Of_Pictures()
        {
            // Arrange
            var results = new List<Picture>
            {
                new Picture { Id = 1, Name = "Pic 1", ImageLink = "link1.jpg", Context = "Context 1" },
                new Picture { Id = 2, Name = "Pic 2", ImageLink = "link2.jpg", Context = "Context 2" }
            };
            var pagedResult = new PagedResult<Picture> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _pictureService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_Should_Return_Picture()
        {
            // Arrange
            var picture = new Picture { Id = 1, Name = "Pic 1", ImageLink = "link1.jpg", Context = "Context 1" };
            _repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(picture);

            // Act
            var result = await _pictureService.Get(1);

            // Assert
            Assert.Equal(picture, result);
        }

        [Fact]
        public async Task Save_Should_Call_Repository_Save()
        {
            // Arrange
            var picture = new Picture { Id = 1, Name = "Pic 1", ImageLink = "link1.jpg", Context = "Context 1" };

            // Act
            await _pictureService.Save(picture);

            // Assert
            _repositoryMock.Verify(r => r.Save(picture), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            int id = 1;

            // Act
            await _pictureService.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}
