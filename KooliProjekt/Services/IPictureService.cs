using KooliProjekt.Data;

namespace YourApp.Services
{
    public interface IPictureService
    {
        // Get all pictures
        Task<List<Picture>> GetAllPicturesAsync();

        // Get picture by ID
        Task<Picture> GetPictureByIdAsync(int id);

        // Add new picture
        Task AddPictureAsync(Picture picture);

        // Update an existing picture
        Task UpdatePictureAsync(Picture picture);

        // Delete picture by ID
        Task DeletePictureAsync(int id);
    }
}
