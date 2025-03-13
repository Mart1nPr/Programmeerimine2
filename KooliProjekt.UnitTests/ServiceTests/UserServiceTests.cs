using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class UserServiceTests : ServiceTestBase
    {
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = new UserService(DbContext);
        }

        [Fact]
        public async Task Create_ShouldAddUser()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "SecurePassword123"
            };

            // Act
            await _userService.Create(user);

            // Assert
            var createdUser = await DbContext.Users.FirstOrDefaultAsync(u => u.Email == "john.doe@example.com");
            Assert.NotNull(createdUser);
            Assert.Equal("John Doe", createdUser.Name);
        }

        [Fact]
        public async Task List_ShouldReturnPagedUsers()
        {
            // Arrange
            var user1 = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "SecurePassword123"
            };
            var user2 = new User
            {
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Password = "SecurePassword123"
            };
            await _userService.Create(user1);
            await _userService.Create(user2);

            var search = new UsersSearch { Keyword = "Doe" };

            // Act
            var result = await _userService.List(1, 10, search);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Results);
            Assert.Equal("John Doe", result.Results.First().Name);
        }

        [Fact]
        public async Task Get_ShouldReturnUserById()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "SecurePassword123"
            };
            await _userService.Create(user);

            // Act
            var fetchedUser = await _userService.Get(user.Id);

            // Assert
            Assert.NotNull(fetchedUser);
            Assert.Equal("John Doe", fetchedUser.Name);
            Assert.Equal("john.doe@example.com", fetchedUser.Email);
        }

        [Fact]
        public async Task Save_ShouldUpdateUser()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "SecurePassword123"
            };
            await _userService.Create(user);

            // Act
            user.Name = "Johnathan Doe";
            await _userService.Save(user);

            // Assert
            var updatedUser = await _userService.Get(user.Id);
            Assert.NotNull(updatedUser);
            Assert.Equal("Johnathan Doe", updatedUser.Name);
            Assert.Equal("john.doe@example.com", updatedUser.Email);
        }

        [Fact]
        public async Task Delete_ShouldRemoveUser()
        {
            // Arrange
            var user = new User
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "SecurePassword123"
            };
            await _userService.Create(user);

            // Act
            await _userService.Delete(user.Id);

            // Assert
            var deletedUser = await DbContext.Users.FindAsync(user.Id);
            Assert.Null(deletedUser);
        }
    }
}
