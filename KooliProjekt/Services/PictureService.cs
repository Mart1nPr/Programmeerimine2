using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class PictureService : IPictureService
    {
        private readonly IUnitOfWork _uof;

        public PictureService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task Delete(int id)
        {
            await _uof.PictureRepository.Delete(id);
        }

        public async Task<Picture> Get(int id)
        {
            return await _uof.PictureRepository.Get(id);
        }

        public Task<PagedResult<Picture>> List(int page, int pageSize)
        {
            return _uof.PictureRepository.List(page, pageSize);
        }

        public async Task Save(Picture picture)
        {
            await _uof.PictureRepository.Save(picture);
        }
    }
}
