using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class FolderService : IFolderService
    {
        private readonly IUnitOfWork _uof;

        public FolderService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task Delete(int id)
        {
            await _uof.FolderRepository.Delete(id);
        }

        public async Task<Folder> Get(int id)
        {
            return await _uof.FolderRepository.Get(id);
        }

        public Task<PagedResult<Folder>> List(int page, int pageSize)
        {
            return _uof.FolderRepository.List(page, pageSize);
        }

        public async Task Save(Folder folder)
        {
            await _uof.FolderRepository.Save(folder);
        }
    }
}
