using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IFolderService
    {
        Task<PagedResult<Folder>> List(int page, int pageSize);
        Task<Folder> Get(int id);
        Task Save(Folder list);
        Task Delete(int id);
    }
}