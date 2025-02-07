namespace KooliProjekt.Data.Repositories
{
    public interface IPictureRepository
    {
        Task<Picture> Get(int id);
        Task<PagedResult<Picture>> List(int page, int pageSize);
        Task Save(Picture item);
        Task Delete(int id);
    }
}