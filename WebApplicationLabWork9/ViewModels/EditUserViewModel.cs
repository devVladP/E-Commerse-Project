using System.ComponentModel.DataAnnotations;

namespace WebApplicationLabWork9.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Емейл обов'язковий")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Нікнейм обов'язковий")]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        
    }
}
