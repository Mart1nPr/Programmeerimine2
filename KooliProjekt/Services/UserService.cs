using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uof;

        public UserService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task Delete(int id)
        {
            await _uof.UserRepository.Delete(id);
        }

        public async Task<User> Get(int id)
        {
            return await _uof.UserRepository.Get(id);
        }

        public Task<PagedResult<User>> List(int page, int pageSize)
        {
            return _uof.UserRepository.List(page, pageSize);
        }

        public async Task Save(User user)
        {
            await _uof.UserRepository.Save(user);
        }
    }
}
