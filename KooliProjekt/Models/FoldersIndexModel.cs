using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class FoldersIndexModel
    {
        public FoldersSearch SearchParams { get; set; }  
        public PagedResult<Folders> Data { get; set; }   
    }
}
