using KooliProjekt.Data;
using KooliProjekt.Search;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IFolderService
    {
        Task<PagedResult<Folder>> List(int page, int pageSize, FoldersSearch search = null);
        Task<Folder> Get(int id);
        Task Save(Folder list);
        Task Delete(int id);
    }
}