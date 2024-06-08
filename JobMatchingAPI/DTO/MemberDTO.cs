namespace JobMatchingAPI.DTO
{
    public class MemberDTO
    {
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set; }
        public string? EMAIL { get; set; }
        public double PhoneNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public MaritalStatus MaritalStatus { get; set; } //enum
        public Gender Gender { get; set; } //enum
        public string? StateOfOrigin { get; set; }
        public string? CurrentState { get; set; }
        public string? Nationality { get; set; } //enum
        public JobPreference JobPreference { get; set; } //enum
    }

    public enum MaritalStatus
    { 
        Single,
        Married,
        PreferNotTell
    }

    public enum Gender
    { 
        Male,
        Female,
        Others
    }

    public enum JobPreference
    {
        Site,
        Hybrid,
        Other
    }
}
