// Services/IFolderService.cs
namespace YourApp.Services
{
    public interface IFolderService
    {
        Task<List<FolderService>> GetAllFoldersAsync();
        Task<FolderService> GetFolderByIdAsync(int id);
        Task AddFolderAsync(FolderService folder);
        Task UpdateFolderAsync(FolderService folder);
        Task DeleteFolderAsync(int id);
    }
}
