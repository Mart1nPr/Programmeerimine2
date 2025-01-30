using System.ComponentModel.DataAnnotations;
namespace KooliProjekt.Data
{
    public class Folder
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        [Required]
        [StringLength(25)]
        public string Description { get; set; }
        public DateTime Creation_date { get; set; } = DateTime.UtcNow;

    }
}