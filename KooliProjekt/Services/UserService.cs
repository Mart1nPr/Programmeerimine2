using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        // Konstruktor, kus võtame vastu IUnitOfWork ja IUserRepository
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        // Kasutaja kustutamine
        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                _userRepository.Delete(user);
                await _unitOfWork.SaveAsync(); // Kõik muudatused salvestatakse transaktsioonis
            }
        }

        // Kasutaja järgi leidmine ID järgi
        public async Task<Users> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        // Kõikide kasutajate toomine
        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        // Kasutaja salvestamine (uus või olemasolev)
        public async Task SaveUserAsync(Users user)
        {
            if (user.Id == 0)
            {
                _userRepository.Add(user);
            }
            else
            {
                _userRepository.Update(user);
            }

            await _unitOfWork.SaveAsync(); // Kõik muudatused salvestatakse transaktsioonis
        }
    }
}
