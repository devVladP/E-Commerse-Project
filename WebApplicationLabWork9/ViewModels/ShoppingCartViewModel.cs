using WebApplicationLabWork9.Models;

namespace WebApplicationLabWork9.ViewModels
{
	public class ShoppingCartViewModel
	{
		public IEnumerable<Product> products { get; set; }
		public IEnumerable<Order> orders { get; set; }
		public decimal totalsum { get; set; }
		public string UserId { get; set; }
	}
}
