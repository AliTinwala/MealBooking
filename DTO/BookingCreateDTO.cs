using System.ComponentModel.DataAnnotations;

namespace MEAL_2024_API.DTO
{
    public class BookingCreateDTO 
    {
        public Guid UserId { get; set; }
        public string MealType { get; set; } // Lunch or Dinner
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var validationResults = new List<ValidationResult>();
        //    var today = DateTime.UtcNow.Date;
        //    if (StartDate < today)
        //    {
        //        validationResults.Add(new ValidationResult("Start date cannot be in the past.", new[] { nameof(StartDate) }));
        //    }

        //    if ((EndDate - StartDate).TotalDays > 30)
        //    {
        //        validationResults.Add(new ValidationResult("The date range cannot exceed 30 days.", new[] { nameof(EndDate) }));
        //    }

        //    if (StartDate < DateTime.UtcNow.AddHours(-22))
        //    {
        //        validationResults.Add(new ValidationResult("Bookings can only be made before 10 PM of the previous day.", new[] { nameof(StartDate) }));
        //    }

        //    return validationResults;
        //}
    }
}
