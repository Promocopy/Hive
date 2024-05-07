using Microsoft.AspNetCore.Identity;

namespace JobMatchingAPI.Entity
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public double PhoneNumber { get; set; } 
        public string? Email { get; set; } = string.Empty;

    }
}
