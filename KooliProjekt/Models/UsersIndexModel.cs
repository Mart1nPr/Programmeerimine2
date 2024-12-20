using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class UsersIndexModel
    {
        public UsersSearch SearchParams { get; set; }  
        public PagedResult<Users> Data { get; set; }    
    }
}
