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
    public class MemberEducationalHistoryController : ControllerBase
    {
        private readonly JobContext jobContext;
        public MemberEducationalHistoryController(JobContext _jobContext)
        {
            jobContext=_jobContext;
        }

        [HttpPost("AddEducationalHistory")]
        public IActionResult AddHistory(MemberEducarionalHistory history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Incorrect information");
            }
            jobContext.EHistories.Add(history);
            jobContext.SaveChanges();
            return Ok(new { message = "Educational History has been added. " });

        }

        [HttpPut("UpdateEducationalHistory")]
        public IActionResult UpdateHistory(MemberEducarionalHistory history)
        {
            var user = jobContext.EHistories.Update(history);
            if (user == null)
            {
                return BadRequest("Incorrect Information,Please check and try again.");
            }
            jobContext.SaveChanges();
            return Ok(new { message = "Educational History has been updated. " });
        }


        [HttpDelete("DeleteEducationalHistory")]
        public IActionResult DeleteEducationalHistory(MemberEducarionalHistory history, string InstitutionName)
        {
            var user = jobContext.EHistories.Where(c => c.InstitutionName == InstitutionName).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("History not Found");
            }
            jobContext.EHistories.Remove(user);
            jobContext.SaveChanges();
            return Ok(new { message = "Educational History has been Deleted. " });
        }



    }
}
