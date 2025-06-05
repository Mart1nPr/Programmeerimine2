using System.Net.Http;
using System.Windows;
using KooliProjekt.PublicAPI.Api;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7136/api/")
            };
            var apiClient = new ApiClient();

            var viewModel = new MainWindowViewModel(apiClient);

            viewModel.ConfirmDelete = _ =>
            {
                var result = MessageBox.Show(
                                "Are you sure you want to delete selected item?",
                                "Delete user",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning);
                return (result == MessageBoxResult.Yes);
            };

            viewModel.OnError = error =>
            {
                MessageBox.Show(
                    error,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            };

            DataContext = viewModel;

            await viewModel.LoadUsers();
        }
    }
}
