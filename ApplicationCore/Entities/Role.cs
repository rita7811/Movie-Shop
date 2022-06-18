using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("Role")]
    public class Role
	{
        // properties
        public int Id { get; set; }
        [MaxLength(20)]
        public string? Name { get; set; }

        // Navigation Properties
        public ICollection<UserRole> UserRolesOfRole { get; set; }
    }
}

