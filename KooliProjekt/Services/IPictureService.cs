using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IPictureService
    {
        Task<PagedResult<Picture>> List(int page, int pageSize);
        Task<Picture> Get(int id);
        Task Save(Picture list);
        Task Delete(int id);
    }
}