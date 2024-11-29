using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IFolderService
    {
        Task<IEnumerable<Folders>> GetAllFoldersAsync();
        Task<Folders> GetFolderByIdAsync(int id);
        Task SaveFolderAsync(Folders folder);
        Task DeleteFolderAsync(int id);
    }
}
