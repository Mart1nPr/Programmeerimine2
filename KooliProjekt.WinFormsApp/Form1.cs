using KooliProjekt.PublicAPI.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form, IUserView
    {
        public IList<User> Users
        {
            get => (IList<User>)UsersGrid.DataSource;
            set => UsersGrid.DataSource = value;
        }

        public User SelectedItem { get; set; }

        public UserPresenter Presenter { get; set; }

        public int Id
        {
            get => int.TryParse(IdField.Text, out var id) ? id : 0;
            set => IdField.Text = value.ToString();
        }

        public string Email
        {
            get => EmailField.Text;
            set => EmailField.Text = value;
        }

        public string Name
        {
            get => NameField.Text;
            set => NameField.Text = value;
        }

        public string Password
        {
            get => PasswordField.Text;
            set => PasswordField.Text = value;
        }

        public DateTime RegistrationTime
        {
            get => RegTimePicker.Value;
            set => RegTimePicker.Value = value;
        }

        public Form1()
        {
            InitializeComponent();

            UsersGrid.AutoGenerateColumns = true;
            UsersGrid.SelectionChanged += UsersGrid_SelectionChanged;

            NewButton.Click += AddButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;

            Load += Form1_Load;
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            await Presenter.Load();
        }

        private void UsersGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (UsersGrid.SelectedRows.Count == 0)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = (User)UsersGrid.SelectedRows[0].DataBoundItem;
            }

            Presenter.UpdateView(SelectedItem);
        }

        private void AddButton_Click(object? sender, EventArgs e)
        {
            Presenter.AddNew();
        }

        private void SaveButton_Click(object? sender, EventArgs e)
        {
            Presenter.Save();
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            Presenter.Delete();
        }
    }

}
