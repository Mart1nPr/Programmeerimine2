using KooliProjekt.PublicAPI.Api;

namespace KooliProjekt.WinFormsApp
{
    public interface IUserView
    {
        IList<User> Users { get; set; }
        User SelectedItem { get; set; }
        UserPresenter Presenter { get; set; }

        int Id { get; set; }
        string Email { get; set; }
        string Name { get; set; }
        string Password { get; set; }
        DateTime RegistrationTime { get; set; }
    }
}
