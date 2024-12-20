using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Pictures
    {
        public int Id { get; set; }

        [Required]
        public string ImageLink { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Context { get; set; }

        [Required]
        public DateTime Creation_date { get; set; } = DateTime.UtcNow;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // Lisame IsDone välja
        public bool? IsDone { get; set; }  // Kasutame nullable bool tüüpi
    }
}
