namespace JobMatchingAPI.Entity
{
    public class MemberEducarionalHistory
    {
        public int Id { get; set; }                                     
        public string? InstitutionName { get; set; }
        public string? CourseOfStudy { get; set; }
        public string? DegreeObtained { get; set; }
        public string? CGPAScore { get; set; }
        public string? DegreeClassification {get; set;}
        public string? StartDate{ get; set;}
        public string? EndDate { get; set;}

    }
}
