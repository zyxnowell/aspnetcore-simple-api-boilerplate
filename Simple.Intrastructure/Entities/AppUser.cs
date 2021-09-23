using Microsoft.AspNetCore.Identity;
using System;


namespace Simple.Intrastructure.Entities
{
    /// <summary>
    /// Identity entity with custom fields
    /// </summary>
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsArchived { get; set; } = false;
        public DateTime CreatedAt { get; set; }
    }
}
