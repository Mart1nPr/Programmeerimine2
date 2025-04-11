using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Api
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