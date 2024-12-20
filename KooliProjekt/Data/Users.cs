using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime Registration_Time { get; set; } = DateTime.UtcNow;

        // Lisame IsDone välja
        public bool? IsDone { get; set; }  // Kasutame nullable bool tüüpi
    }
}
