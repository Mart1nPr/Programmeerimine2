using KooliProjekt.Data;

public interface IFolderRepository
{
    Task<Folders> GetById(int id);
    Task<PagedResult<Folders>> List(int page, int pageSize);
    Task Save(Folders folder);
    Task Delete(int id);
}