using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Pictures
    {
        public int Id { get; set; }

        [Required]
        public byte[] ImageData { get; set; }  

        [Required]
        [StringLength(100)] 
        public string Name { get; set; }

        [Required]
        [StringLength(500)] 
        public string Context { get; set; }

        [Required]
        public DateTime Creation_date { get; set; } = DateTime.UtcNow;  

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
