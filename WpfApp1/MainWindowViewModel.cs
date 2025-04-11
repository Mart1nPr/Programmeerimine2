using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp1.Api;
using WpfApp1;

public class UserViewModel : NotifyPropertyChangedBase
{
    public ObservableCollection<User> Users { get; private set; }
    public ICommand NewCommand { get; private set; }
    public ICommand SaveCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }
    public Predicate<User> ConfirmDelete { get; set; }
    public Action<string> OnError { get; set; }

    private readonly IApiClient _apiClient;

    public UserViewModel() : this(new ApiClient()) { }

    public UserViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;

        Users = new ObservableCollection<User>();

        NewCommand = new RelayCommand<User>(
            user =>
            {
                SelectedUser = new User();  
            }
        );

        SaveCommand = new RelayCommand<User>(
            async user =>
            {
                if (SelectedUser.Id == 0)
                {
                  
                    await _apiClient.Save(SelectedUser);
                }
                else
                {
                    
                    await _apiClient.Save(SelectedUser);
                }
                await Load();
            },
            user => SelectedUser != null  
        );

       
        DeleteCommand = new RelayCommand<User>(
            async user =>
            {
                if (ConfirmDelete != null)
                {
                    var result = ConfirmDelete(SelectedUser);
                    if (!result) return;
                }

                await _apiClient.Delete(SelectedUser.Id);  
                Users.Remove(SelectedUser);  
                SelectedUser = null;  
            },
            user => SelectedUser != null
        );
    }

    public async Task Load()
    {
        Users.Clear();
        var users = await _apiClient.List(); 

        if (users == null)
        {
            OnError?.Invoke("Failed to load users.");
            return;
        }

        foreach (var user in users)
        {
            Users.Add(user);  
        }
    }

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
}
