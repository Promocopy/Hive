namespace JobMatchingAPI.Entity
{
    public class MemberWorkHistory
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Industry { get; set; }
        public string? JobPosition { get; set; }
        public string? StartDate { get; set;}
        public string? EndDate { get; set;}
        public string? SummaryOfJobDescription { get; set; }
    }
}
