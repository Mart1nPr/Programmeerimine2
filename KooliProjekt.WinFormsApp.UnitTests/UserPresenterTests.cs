using Moq;
using Xunit;
using KooliProjekt.PublicAPI.Api;

namespace KooliProjekt.WinFormsApp.UnitTests
{
    public class UserPresenterTests
    {
        private readonly Mock<IUserView> _viewMock;
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly Mock<Action<string>> _showErrorMock;
        private readonly UserPresenter _presenter;

        public UserPresenterTests()
        {
            _viewMock = new Mock<IUserView>();
            _apiClientMock = new Mock<IApiClient>();
            _showErrorMock = new Mock<Action<string>>();
            _presenter = new UserPresenter(_viewMock.Object, _apiClientMock.Object, _showErrorMock.Object);
        }

        [Fact]
        public void UpdateView_WithNullUser_ResetsFields()
        {
            // Act
            _presenter.UpdateView(null);

            // Assert
            _viewMock.VerifySet(v => v.Id = 0);
            _viewMock.VerifySet(v => v.Email = string.Empty);
            _viewMock.VerifySet(v => v.Name = string.Empty);
            _viewMock.VerifySet(v => v.Password = string.Empty);
            _viewMock.VerifySet(v => v.RegistrationTime = It.IsAny<DateTime>());
        }

        [Fact]
        public void UpdateView_WithUser_SetsFieldsCorrectly()
        {
            var user = new User
            {
                Id = 1,
                Email = "test@example.com",
                Name = "Test",
                Password = "secret",
                Registration_Time = new DateTime(2022, 1, 1)
            };

            // Act
            _presenter.UpdateView(user);

            // Assert
            _viewMock.VerifySet(v => v.Id = 1);
            _viewMock.VerifySet(v => v.Email = "test@example.com");
            _viewMock.VerifySet(v => v.Name = "Test");
            _viewMock.VerifySet(v => v.Password = "secret");
            _viewMock.VerifySet(v => v.RegistrationTime = new DateTime(2022, 1, 1));
        }

        [Fact]
        public void AddNew_CallsUpdateViewWithNull()
        {
            _presenter.AddNew();
            _viewMock.VerifySet(v => v.Id = 0);
        }

        [Fact]
        public async Task Load_WhenApiReturnsSuccess_SetsUsers()
        {
            var users = new List<User> { new User { Id = 1, Name = "Test" } };
            _apiClientMock.Setup(api => api.List())
                .ReturnsAsync(Result<List<User>>.Ok(users));

            await _presenter.Load();

            _viewMock.VerifySet(v => v.Users = users);
        }

        [Fact]
        public async Task Load_WhenApiReturnsError_CallsShowError()
        {
            // Arrange
            var result = Result<List<User>>.Fail("Test error");
            _apiClientMock.Setup(api => api.List()).ReturnsAsync(result);

            // Act
            await _presenter.Load();

            // Assert
            _showErrorMock.Verify(action => action("Test error"), Times.Once);
        }

    }
}
