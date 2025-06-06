using Moq;
using KooliProjekt.PublicAPI.Api;

namespace WpfApp.UnitTests
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
        public async Task LoadUsers_ShouldPopulateUsers_WhenApiReturnsData()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "Alice", Email = "alice@example.com" },
                new User { Id = 2, Name = "Bob", Email = "bob@example.com" }
            };

            _mockApiClient
                .Setup(api => api.List())
                .ReturnsAsync(Result<List<User>>.Success(users));

            // Act
            await _viewModel.LoadUsers();

            // Assert
            Assert.Equal(2, _viewModel.Users.Count);
            Assert.Equal("Alice", _viewModel.Users[0].Name);
            Assert.Equal("Bob", _viewModel.Users[1].Name);
        }

        [Fact]
        public async Task LoadUsers_ShouldCallOnError_WhenApiReturnsError()
        {
            // Arrange
            string error = null;
            _viewModel.OnError = msg => error = msg;

            _mockApiClient
                .Setup(api => api.List())
                .ReturnsAsync(Result<List<User>>.Failure("API failed"));

            // Act
            await _viewModel.LoadUsers();

            // Assert
            Assert.Equal("Error while loading users: API failed", error);
        }

        [Fact]
        public async Task SaveCommand_ShouldCallSaveAndReload_WhenSelectedUserIsValid()
        {
            // Arrange
            var user = new User { Id = 0, Name = "New User", Email = "new@example.com" };
            _viewModel.SelectedUser = user;

            _mockApiClient
                .Setup(api => api.Save(user))
                .ReturnsAsync(Result.Success());

            _mockApiClient
                .Setup(api => api.List())
                .ReturnsAsync(Result<List<User>>.Success(new List<User>()));

            // Act
            await Task.Run(() => _viewModel.SaveCommand.Execute(user));

            // Assert
            _mockApiClient.Verify(api => api.Save(user), Times.Once);
            _mockApiClient.Verify(api => api.List(), Times.Once);
        }

        [Fact]
        public async Task DeleteCommand_ShouldCallDeleteAndRemoveUser_WhenConfirmed()
        {
            // Arrange
            var user = new User { Id = 5, Name = "Delete Me" };
            _viewModel.Users.Add(user);
            _viewModel.SelectedUser = user;
            _viewModel.ConfirmDelete = u => true;

            _mockApiClient
                .Setup(api => api.Delete(user.Id))
                .ReturnsAsync(Result.Success());

            // Act
            await Task.Run(() => _viewModel.DeleteCommand.Execute(user));

            // Assert
            _mockApiClient.Verify(api => api.Delete(user.Id), Times.Once);
            Assert.DoesNotContain(user, _viewModel.Users);
            Assert.Null(_viewModel.SelectedUser);
        }

        [Fact]
        public void DeleteCommand_ShouldNotCallDelete_WhenConfirmationFails()
        {
            // Arrange
            var user = new User { Id = 3, Name = "Protected" };
            _viewModel.Users.Add(user);
            _viewModel.SelectedUser = user;
            _viewModel.ConfirmDelete = u => false;

            // Act
            _viewModel.DeleteCommand.Execute(user);

            // Assert
            _mockApiClient.Verify(api => api.Delete(It.IsAny<int>()), Times.Never);
            Assert.Equal(user, _viewModel.SelectedUser);
        }

        [Fact]
        public void NewCommand_ShouldCreateNewUser()
        {
            // Act
            _viewModel.NewCommand.Execute(null);

            // Assert
            Assert.NotNull(_viewModel.SelectedUser);
            Assert.Equal(0, _viewModel.SelectedUser.Id);
        }

        [Fact]
        public async Task SaveCommand_ShouldCallOnError_WhenSaveReturnsError()
        {
            // Arrange
            string error = null;
            var user = new User { Id = 0, Name = "Error User" };
            _viewModel.SelectedUser = user;
            _viewModel.OnError = msg => error = msg;

            _mockApiClient
                .Setup(api => api.Save(user))
                .ReturnsAsync(Result.Failure("Save failed"));

            // Act
            await Task.Run(() => _viewModel.SaveCommand.Execute(user));

            // Assert
            Assert.Equal("Error while saving user: Save failed", error);
        }

        [Fact]
        public async Task DeleteCommand_ShouldCallOnError_WhenDeleteReturnsError()
        {
            // Arrange
            string error = null;
            var user = new User { Id = 10 };
            _viewModel.Users.Add(user);
            _viewModel.SelectedUser = user;
            _viewModel.ConfirmDelete = u => true;
            _viewModel.OnError = msg => error = msg;

            _mockApiClient
                .Setup(api => api.Delete(user.Id))
                .ReturnsAsync(Result.Failure("Delete failed"));

            // Act
            await Task.Run(() => _viewModel.DeleteCommand.Execute(user));

            // Assert
            Assert.Equal("Error while deleting user: Delete failed", error);
        }
    }
}
