using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class PicturesIndexModel
    {
        public PicturesSearch SearchParams { get; set; } 
        public PagedResult<Pictures> Data { get; set; }    
    }
}
