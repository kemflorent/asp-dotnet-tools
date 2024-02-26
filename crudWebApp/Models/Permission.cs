using System;
using System.ComponentModel.DataAnnotations;

namespace CrudWebApp.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedAt { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();
    }
}