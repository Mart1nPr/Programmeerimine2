using System.ComponentModel.DataAnnotations;
namespace KooliProjekt.Data
{
    public class Picture : Entity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageLink { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Context { get; set; }

        public DateTime Creation_date { get; set; } = DateTime.UtcNow;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}