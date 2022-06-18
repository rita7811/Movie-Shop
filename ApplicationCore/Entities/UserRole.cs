using System;

namespace ApplicationCore.Entities
{
    public class UserRole
	{
        // properties
        public int UserId { get; set; }
        public int RoleId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Role Role { get; set; }
    }
}

