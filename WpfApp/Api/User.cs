using System.ComponentModel.DataAnnotations;

namespace WpfApp.Api
{
    public class User
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