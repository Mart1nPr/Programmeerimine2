using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.PublicAPI.Api
{
    public interface IApiClient
    {
        Task<Result<List<User>>> List();
        Task<Result<User>> Get(int id);
        Task<Result> Save(User list);
        Task<Result> Delete(int id);
    }

}
