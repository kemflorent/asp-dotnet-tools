using System;
using System.Collections; 
using System.ComponentModel.DataAnnotations;

namespace CrudWebApp.Models
{
    // [Table("User")]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Role? Role { get; set; }

        internal List<string> Permissions { get; set; } = new List<string>();
    }
}

// [Column(TypeName : )]
// [Required]