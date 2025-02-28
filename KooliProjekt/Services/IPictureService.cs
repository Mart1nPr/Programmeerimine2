using KooliProjekt.Data;
using KooliProjekt.Search;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IPictureService
    {
        Task<PagedResult<Picture>> List(int page, int pageSize, PicturesSearch search = null);
        Task Create(Picture picture);
        Task<Picture> Get(int id);
        Task Save(Picture list);
        Task Delete(int id);
    }
}