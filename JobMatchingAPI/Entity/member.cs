using JobMatchingAPI.Entity;

namespace JobMatchingAPI.Entity
{
    public class Member
    {
        public int Id { get; set; }
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set; }
        public string? EMAIL { get; set; }
        public double PhoneNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public string? MaritalStatus { get; set; } //enum
        public string? Gender { get; set; } //enum
        public string? StateOfOrigin { get; set; }
        public string? CurrentState { get; set; }
        public string? Nationality { get; set; } //enum
        public string? JobPreference { get; set; } //enum
    }
}
