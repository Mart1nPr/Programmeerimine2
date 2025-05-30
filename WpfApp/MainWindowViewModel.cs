using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp.Api;

namespace WpfApp
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public ObservableCollection<User> Users { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Predicate<User> ConfirmDelete { get; set; }
        public Action<string> OnError { get; set; }

        private readonly IApiClient _apiClient;

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                NotifyPropertyChanged();
            }
        }

        public MainWindowViewModel() : this(new ApiClient())
        {
        }

        public MainWindowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
            Users = new ObservableCollection<User>();

            NewCommand = new RelayCommand<User>(
                user => { SelectedUser = new User(); }
            );

            SaveCommand = new RelayCommand<User>(
                async user =>
                {
                    var result = await _apiClient.Save(SelectedUser);
                    if (result.HasError)
                    {
                        OnError?.Invoke($"Error while saving user: {result.Error}");
                        return;
                    }

                    await LoadUsers();
                },
                user => SelectedUser != null
            );

            DeleteCommand = new RelayCommand<User>(
                async user =>
                {
                    if (ConfirmDelete?.Invoke(SelectedUser) ?? true)
                    {
                        var result = await _apiClient.Delete(SelectedUser.Id);
                        if (result.HasError)
                        {
                            OnError?.Invoke($"Error while deleting user: {result.Error}");
                            return;
                        }

                        Users.Remove(SelectedUser);
                        SelectedUser = null;
                    }
                },
                user => SelectedUser != null
            );
        }

        public async Task LoadUsers()
        {
            Users.Clear();

            var result = await _apiClient.List();
            if (result.HasError)
            {
                OnError?.Invoke($"Error while loading users: {result.Error}");
                return;
            }

            foreach (var user in result.Value)
            {
                Users.Add(user);
            }
        }
    }
}
