using JobMatchingAPI.Data;
using JobMatchingAPI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobMatchingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkHistoryController : ControllerBase
    {
        private readonly JobContext context;
        public WorkHistoryController(JobContext _context)
        {
            context = _context;
        }

        [HttpPost("AddWorkHistory")]
        public IActionResult AddWorkHistory(MemberWorkHistory History)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Please put in correct Information");
            }
            context.WorkHistories.Add(History);
            context.SaveChanges();
            return Ok(new { message = "Work history added successfully" });
        
        }

        [HttpPut("UpdateWorkHistory")]
        public IActionResult UpdateWorkHistory(MemberWorkHistory History)
        {
            var user = context.WorkHistories.Update(History);
            if(user == null)
            {
                return BadRequest("Incorrect Information");
            }
            context.SaveChanges();
            return Ok(new { message = "Work history updated successfully" });
        }

        [HttpDelete("DeleteWorkHistory")]
        public IActionResult DeleteWorkHistory(MemberWorkHistory History, string CompanyName)
        {
            var user = context.WorkHistories.Where(c=>c.CompanyName== CompanyName).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            context.WorkHistories.Remove(user);
            context.SaveChanges();
            return Ok(new { message = "Work history deleted successfully" });
        }
    }
}
