using System.ComponentModel.DataAnnotations;

namespace WebApplicationLabWork9.ViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name = "Email adress")]
        [Required(ErrorMessage = "Емейл обов'язковий")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Підтвердження паролю обов'язково")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmedPassword { get; set; }

        [Required(ErrorMessage = "Ім'я користувача обов'язкове")]
        public string UserName { get; set; }
    }
}
