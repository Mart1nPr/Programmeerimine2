using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Api
{
    public interface IApiClient
    {
        Task<List<User>> List();
        Task Save(User list);
        Task Delete(int id);
    }
}