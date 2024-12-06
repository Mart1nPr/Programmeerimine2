using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Data
{
    public interface IUnitOfWork
    {
        IUsersRepository Users { get; }
        Task<int> CompleteAsync();
    }
}
