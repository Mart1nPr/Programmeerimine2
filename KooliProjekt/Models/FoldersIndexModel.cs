using KooliProjekt.Search;
using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class FoldersIndexModel
    {
        public FoldersSearch Search { get; set; }
        public PagedResult<Folder> Data { get; set; }
    }
}