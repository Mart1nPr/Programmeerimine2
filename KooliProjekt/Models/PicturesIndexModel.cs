using KooliProjekt.Search;
using KooliProjekt.Data;

namespace KooliProjekt.Models
{
    public class PicturesIndexModel
    {
        public PicturesSearch Search { get; set; }
        public PagedResult<Picture> Data { get; set; }
    }
}