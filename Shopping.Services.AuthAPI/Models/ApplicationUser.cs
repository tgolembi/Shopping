using Microsoft.AspNetCore.Identity;

namespace Shopping.Services.AuthAPI.Models
{
	public class ApplicationUser : IdentityUser
	{
		public required string FullName {  get; set; }
	}
}
