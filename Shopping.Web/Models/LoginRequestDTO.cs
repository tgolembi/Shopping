using System.ComponentModel.DataAnnotations;

namespace Shopping.Web.Models
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
