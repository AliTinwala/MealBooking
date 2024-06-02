using System.ComponentModel.DataAnnotations;

namespace MEAL_2024_API.Models
{
    public class FeedbackModel

    { 
        [Key]
        public Guid  FeedbackId { get; set; }

        public Guid UserId { get; set; }

        public User? User { get; set; }

        public string Message { get; set; }
        public int Stars { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
