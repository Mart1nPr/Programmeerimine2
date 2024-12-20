namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IPictureRepository Pictures { get; }
        IFolderRepository Folders { get; }

        Task<int> CompleteAsync();  
        Task BeginTransaction();   
        Task Commit();          
        Task Rollback();      
    }
}
