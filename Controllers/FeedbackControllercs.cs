using MEAL_2024_API.Context;
using MEAL_2024_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MEAL_2024_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackControllercs:ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public FeedbackControllercs(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackModel feedback)
        {
            if (feedback == null)
            {
                return BadRequest();
            }

            feedback.FeedbackId = Guid.NewGuid();   

            _dbContext.Feedbacks.Add(feedback);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Feedback submitted successfully" });
        }

    }
}
