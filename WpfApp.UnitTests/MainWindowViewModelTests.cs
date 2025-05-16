using KooliProjekt.WpfApp;
using KooliProjekt.WpfApp.Api;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.WpfApp.UnitTests
{
    public class MainWindowViewModelTests
    {
        private readonly Mock<IApiClient> _mockApiClient;
        private readonly MainWindowViewModel _viewModel;

        public MainWindowViewModelTests()
        {
            _mockApiClient = new Mock<IApiClient>();
            _viewModel = new MainWindowViewModel(_mockApiClient.Object);
        }

        [Fact]
        public async Task Load_ShouldPopulateLists_WhenApiClientReturnsUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Email = "user1@example.com", Name = "User 1", Password = "password1" },
                new User { Id = 2, Email = "user2@example.com", Name = "User 2", Password = "password2" }
            };

            _mockApiClient.Setup(api => api.List()).ReturnsAsync(users);

            // Act
            await _viewModel.Load();

            // Assert
            Assert.Equal(2, _viewModel.Lists.Count);
            Assert.Equal("user1@example.com", _viewModel.Lists[0].Email);
            Assert.Equal("user2@example.com", _viewModel.Lists[1].Email);
        }

        [Fact]
        public async Task SaveCommand_ShouldSaveNewUser_WhenSelectedItemIsNotNull()
        {
            // Arrange
            var user = new User { Id = 0, Email = "newuser@example.com", Name = "New User", Password = "newpassword" };
            _viewModel.SelectedItem = user;

            // Act
            _viewModel.SaveCommand.Execute(user);

            // Assert
            _mockApiClient.Verify(api => api.Save(user), Times.Once);
        }

        [Fact]
        public async Task DeleteCommand_ShouldDeleteUser_WhenConfirmDeleteReturnsTrue()
        {
            // Arrange
            var user = new User { Id = 1, Email = "user1@example.com", Name = "User 1", Password = "password1" };
            _viewModel.SelectedItem = user;

            _viewModel.ConfirmDelete = u => true;

            // Act
            _viewModel.DeleteCommand.Execute(user);

            // Assert
            _mockApiClient.Verify(api => api.Delete(user.Id), Times.Once);
            Assert.Null(_viewModel.SelectedItem);
        }

        [Fact]
        public async Task DeleteCommand_ShouldNotDeleteUser_WhenConfirmDeleteReturnsFalse()
        {
            // Arrange
            var user = new User { Id = 1, Email = "user1@example.com", Name = "User 1", Password = "password1" };
            _viewModel.SelectedItem = user;

            _viewModel.ConfirmDelete = u => false;

            // Act
            _viewModel.DeleteCommand.Execute(user);

            // Assert
            _mockApiClient.Verify(api => api.Delete(It.IsAny<int>()), Times.Never);
            Assert.Equal(user, _viewModel.SelectedItem);
        }

        [Fact]
        public void NewCommand_ShouldCreateNewUser_WhenExecuted()
        {
            // Act
            _viewModel.NewCommand.Execute(null);

            // Assert
            Assert.NotNull(_viewModel.SelectedItem);
            Assert.Equal(0, _viewModel.SelectedItem.Id);
        }
    }
}
