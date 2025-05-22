using KooliProjekt.Data;
using KooliProjekt.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data.Repositories;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_uowMock.Object);

            _uowMock.SetupGet(u => u.UserRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_Should_Return_List_Of_Users()
        {
            // Arrange
            var results = new List<User>
            {
                new User { Id = 1, Name = "user1", Email = "user1@example.com" },
                new User { Id = 2, Name = "user2", Email = "user2@example.com" }
            };
            var pagedResult = new PagedResult<User> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _userService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_Should_Return_User()
        {
            // Arrange
            var user = new User { Id = 1, Name = "user1", Email = "user1@example.com" };
            _repositoryMock.Setup(r => r.Get(1)).ReturnsAsync(user);

            // Act
            var result = await _userService.Get(1);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task Save_Should_Call_Repository_Save()
        {
            // Arrange
            var user = new User { Id = 1, Name = "user1", Email = "user1@example.com" };

            // Act
            await _userService.Save(user);

            // Assert
            _repositoryMock.Verify(r => r.Save(user), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            // Arrange
            int id = 1;

            // Act
            await _userService.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Delete(id), Times.Once);
        }
    }
}
