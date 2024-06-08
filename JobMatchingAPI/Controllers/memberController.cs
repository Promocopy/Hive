using JobMatchingAPI.Data;
using JobMatchingAPI.DTO;
using JobMatchingAPI.Entity;
using JobMatchingAPI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobMatchingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class memberController : ControllerBase
    {
        private readonly JobContext _jobContext;
        public memberController(JobContext jobContext)
        {
            _jobContext = jobContext;
        }

        [HttpPost("AddMemberProfile")]
        public IActionResult AddPersonalInformation(MemberDTO memberDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("details not Valid. Try Again");
            }
            var member = new Member
            {
                FIRSTNAME = memberDto.FIRSTNAME,
                LASTNAME = memberDto.LASTNAME,
                EMAIL = memberDto.EMAIL,
                PhoneNumber = memberDto.PhoneNumber,
                CurrentState = memberDto.CurrentState,
                DateOfBirth = memberDto.DateOfBirth,
                JobPreference = UtilityHelp.convertToString(memberDto.JobPreference),//memberDto.JobPreference,
                MaritalStatus = UtilityHelp.convertToString(memberDto.MaritalStatus),
                Nationality = memberDto.Nationality,
                StateOfOrigin = memberDto.StateOfOrigin,
                Gender = UtilityHelp.convertToString(memberDto.Gender),

            };
            _jobContext.Member.Add(member);
            _jobContext.SaveChanges();
            return Ok(new { message = "member Added successfully" });
        }

        [HttpPut("UpdateMemberProfile")]
        public IActionResult UpdatePersonalInformation(MemberDTO memberDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("detail not found");
            }
            var member = new Member
            {
                FIRSTNAME = memberDto.FIRSTNAME,
                LASTNAME = memberDto.LASTNAME,
                EMAIL = memberDto.EMAIL,
                PhoneNumber = memberDto.PhoneNumber,
                CurrentState = memberDto.CurrentState,
                DateOfBirth = memberDto.DateOfBirth,
                JobPreference = UtilityHelp.convertToString(memberDto.JobPreference),
                MaritalStatus = UtilityHelp.convertToString(memberDto.MaritalStatus),
                Nationality = memberDto.Nationality,
                StateOfOrigin = memberDto.StateOfOrigin,
                Gender = UtilityHelp.convertToString(memberDto.Gender),

            };
            if (member == null)
            {
                return BadRequest("Member not Found");
            }
            _jobContext.Member.Update(member);
            _jobContext.SaveChanges();
            return Ok(new { message = "member updated successfully" });

        }

        [HttpDelete("DeleteMemberProfile")]
        public IActionResult DeletePersonalInformation(MemberDTO dTO , string email)
        {
            var user = _jobContext.Member.Where(c => c.EMAIL == email ).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            _jobContext.Member.Remove(user);
            _jobContext.SaveChanges();
            return Ok(new {message = "Member is deleted successfully."});
        }

        //[HttpGet("Location")]
        //public ActionResult<Member> GetLocation(string Nationality)
        //{
        //    var user = _jobContext.Member.Find(Nationality);
        //    if (user == null)
        //    {
        //        return BadRequest("Location not found");
        //    }
        //    return Ok(Nationality);
        //}

        //[HttpGet]
        //public ActionResult<Member> GetJobPreference (string JobPreference)
        //{
        //    var user = _jobContext.Member.Find(JobPreference);
        //    if (user == null)
        //    {
        //        return BadRequest("Job preference not found.");
        //    }
        //    return Ok(JobPreference);
        //}

        //[HttpGet]
        //public ActionResult<Member> 
    }
}
