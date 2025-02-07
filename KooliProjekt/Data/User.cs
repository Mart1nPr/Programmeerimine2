using System.ComponentModel.DataAnnotations;
namespace KooliProjekt.Data
{
    public class User : Entity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        public string Password { get; set; }

        public DateTime Registration_Time { get; set; } = DateTime.UtcNow;
    }
}