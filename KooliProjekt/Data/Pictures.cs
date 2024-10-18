using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Pictures
    {
        [Required]
        [StringLength(25)]
        public int Id { get; set; }
        [Required]
        public int Folder_id { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Context { get; set; }
        [Required]
        public DateTime Creation_date { get; set; } = DateTime.UtcNow;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
