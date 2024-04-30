using Microsoft.AspNetCore.Identity;

namespace JobMatchingAPI.Entity
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public double PhoneNumber { get; set; }
        public string? Email { get; set; }

    }
}
