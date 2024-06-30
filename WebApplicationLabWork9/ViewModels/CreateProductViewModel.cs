using System.ComponentModel.DataAnnotations;

namespace WebApplicationLabWork9.ViewModels
{
	public class CreateProductViewModel
	{
		[Display(Name = "Product name")]
		[Required(ErrorMessage = "Product Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Category is required")]
		public string Category { get; set; }

		[Display(Name = "Product price")]
		[Required(ErrorMessage = "Product Price is required")]
		public decimal Price { get; set; }

		[Display(Name = "Product Image")]
		[Required(ErrorMessage = "Product Image is required")]
		public string Image { get; set; }
	}
}
