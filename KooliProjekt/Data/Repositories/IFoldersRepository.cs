namespace KooliProjekt.Data.Repositories
{
    public interface IFoldersRepository
    {
        Task<Folders> Get(int id);
        Task<PagedResult<Folders>> List(int page, int pageSize);
        Task Save(Folders item);
        Task Delete(int id);
    }
}