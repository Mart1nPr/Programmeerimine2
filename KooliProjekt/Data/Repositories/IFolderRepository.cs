namespace KooliProjekt.Data.Repositories
{
    public interface IFolderRepository
    {
        Task<Folder> Get(int id);
        Task<PagedResult<Folder>> List(int page, int pageSize);
        Task Save(Folder item);
        Task Delete(int id);
    }
}