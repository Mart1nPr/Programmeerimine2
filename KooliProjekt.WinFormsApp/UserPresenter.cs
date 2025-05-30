using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public class UserPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IUserView _userView;

        public UserPresenter(IUserView userView, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _userView = userView;
            userView.Presenter = this;
        }

        public void UpdateView(User user)
        {
            if (user == null)
            {
                _userView.Id = 0;
                _userView.Email = string.Empty;
                _userView.Name = string.Empty;
                _userView.Password = string.Empty;
                _userView.RegistrationTime = DateTime.UtcNow;
            }
            else
            {
                _userView.Id = user.Id;
                _userView.Email = user.Email;
                _userView.Name = user.Name;
                _userView.Password = user.Password;
                _userView.RegistrationTime = user.Registration_Time;
            }
        }

        public async Task Load()
        {
            var result = await _apiClient.List();

            if (result.IsSuccess)
            {
                _userView.Users = result.Value;
            }
            else
            {
                MessageBox.Show(result.Error ?? "Tekkis viga kasutajate laadimisel.");
            }
        }

        public void AddNew()
        {
            UpdateView(null);
        }

        public async void Save()
        {
            var user = new User
            {
                Id = _userView.Id,
                Email = _userView.Email,
                Name = _userView.Name,
                Password = _userView.Password,
                Registration_Time = _userView.RegistrationTime
            };

            var result = await _apiClient.Save(user);
            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Error ?? "Salvestamine ebaõnnestus.");
            }

            await Load();
        }

        public async void Delete()
        {
            if (_userView.Id == 0)
            {
                MessageBox.Show("Vali kustutatav kasutaja.");
                return;
            }

            var result = await _apiClient.Delete(_userView.Id);
            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Error ?? "Kustutamine ebaõnnestus.");
            }

            await Load();
        }
    }
}
