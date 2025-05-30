using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using KooliProjekt.WinFormsApp;
using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp.UnitTests
{
    public class UserPresenterTests
    {
        private readonly Mock<IUserView> _viewMock;
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly UserPresenter _presenter;

        public UserPresenterTests()
        {
            _viewMock = new Mock<IUserView>();
            _apiClientMock = new Mock<IApiClient>();
            _presenter = new UserPresenter(_viewMock.Object, _apiClientMock.Object);
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
        public async Task Load_WhenApiReturnsError_ShowsErrorMessage()
        {
            var result = Result<List<User>>.Fail("Test error");
            _apiClientMock.Setup(api => api.List()).ReturnsAsync(result);

            var messageBoxShown = false;
            System.Windows.Forms.MessageBoxManager.OverrideShow((text, caption, buttons) =>
            {
                messageBoxShown = text.Contains("Test error");
                return System.Windows.Forms.DialogResult.OK;
            });

            await _presenter.Load();

            Assert.True(messageBoxShown);
        }
    }
}
