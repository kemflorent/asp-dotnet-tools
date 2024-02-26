using System;
using System.Collections; 
using System.ComponentModel.DataAnnotations;

namespace CrudWebApp.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedAt { get; set; }

        public List<Permission> Permissions { get; set; } = new List<Permission>();
    }
}