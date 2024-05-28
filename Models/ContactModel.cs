using System.ComponentModel.DataAnnotations;

namespace MEAL_2024_API.Models
{
    public class ContactModel
    {
        [Key]
        public int id {  get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
