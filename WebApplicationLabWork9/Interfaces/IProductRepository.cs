using WebApplicationLabWork9.Models;

namespace WebApplicationLabWork9.Interfaces
{
	public interface IProductRepository
	{
		Task<IEnumerable<Product>> GetAll();
		Task<Product> GetByIdAsync(int? id);
		//Task<IEnumerable<Product>> GetProductByCategory(string category);
		bool Add(Product? product);
		bool Update(Product? product);
		bool Delete(Product? product);
		bool Save();
	}
}
