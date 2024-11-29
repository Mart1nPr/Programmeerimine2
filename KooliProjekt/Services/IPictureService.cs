using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IPictureService
    {
        Task<IEnumerable<Pictures>> GetAllPicturesAsync();
        Task<Pictures> GetPictureByIdAsync(int id);
        Task SavePictureAsync(Pictures picture);
        Task DeletePictureAsync(int id);
    }
}
