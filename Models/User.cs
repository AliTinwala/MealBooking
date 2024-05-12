using System.ComponentModel.DataAnnotations;

namespace MEAL_2024_API.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        public string Role { get; set; }

        public int? EmpId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public DateTime? RegistrationDate { get; set; } 

        public DateTime? ModifiedDate { get; set; }

    }
}
