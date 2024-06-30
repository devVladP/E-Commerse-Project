using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLabWork9.Models
{
	public class AppUser : IdentityUser
	{
		public string? FirstName { get; set; } = string.Empty;
		public string? LastName { get; set;} = string.Empty;
		public string? Gender { get; set; } = string.Empty;
		public int? Age { get; set; }
	}
}
