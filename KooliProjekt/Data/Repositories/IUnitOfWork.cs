namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();

        IUserRepository UserRepository { get; }
        IFolderRepository FolderRepository { get; }
        IPictureRepository PictureRepository { get; }
    }
}
