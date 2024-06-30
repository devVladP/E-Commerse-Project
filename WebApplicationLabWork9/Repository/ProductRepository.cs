using Microsoft.EntityFrameworkCore;
using WebApplicationLabWork9.Data;
using WebApplicationLabWork9.Interfaces;
using WebApplicationLabWork9.Models;

namespace WebApplicationLabWork9.Repository
{
    public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _context;
		public ProductRepository(ApplicationDbContext context) 
		{
			_context = context;
		}
		public bool Add(Product? product)
		{
			if (product != null && !_context.Products.Contains(product)) 
			{
				_context.Products.Add(product);
			}

			return Save();
		}

		public bool Delete(Product? product)
		{
			if (product != null)
			{
				_context.Products.Remove(product);
			}

			return Save();
		}

		public async Task<IEnumerable<Product>> GetAll()
		{
			return await _context.Products.ToListAsync();
		}

		public async Task<Product> GetByIdAsync(int? id)
		{
			return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
		}

		//public async Task<IEnumerable<Product>> GetProductByCategory(string category)
		//{
		//	return await _context.Products.Where(p => p.Category == category).ToListAsync();
		//}

		public bool Save()
		{
			var save = _context.SaveChanges();
			return save > 0;
		}

		public bool Update(Product? product)
		{
			if (product is not null)
			{
				_context.Products.Update(product);
			}

			return Save();
		}
	}
}
