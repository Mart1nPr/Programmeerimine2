using KooliProjekt.Data;

public interface IPictureRepository
{
    Task<Pictures> GetById(int id);
    Task<PagedResult<Pictures>> List(int page, int pageSize);
    Task Save(Pictures picture);
    Task Delete(int id);
}