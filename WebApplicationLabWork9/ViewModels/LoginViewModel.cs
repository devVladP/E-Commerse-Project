using System.ComponentModel.DataAnnotations;

namespace WebApplicationLabWork9.ViewModels
{
	public class LoginViewModel
	{
		[Display(Name = "Email Address")]
		[Required(ErrorMessage = "Емейл обов'язковий")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обов'язковий")]
        [DataType(DataType.Password)]
		public string Password { get; set; }
    }
}
