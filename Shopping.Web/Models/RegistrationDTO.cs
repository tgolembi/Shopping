using System.ComponentModel.DataAnnotations;

namespace Shopping.Web.Models
{
    public class RegistrationDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
