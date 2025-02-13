using KooliProjekt.Search;
using KooliProjekt.Data;
namespace KooliProjekt.Models
{
    public class FoldersIndexModel
    {
        public FoldersSearch Search { get; set; }
        public PagedResult<Folder> Data { get; set; }
    }
}