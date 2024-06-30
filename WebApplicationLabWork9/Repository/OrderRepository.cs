using Microsoft.EntityFrameworkCore;
using WebApplicationLabWork9.Data;
using WebApplicationLabWork9.Interfaces;
using WebApplicationLabWork9.Models;

namespace WebApplicationLabWork9.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _context;
		public OrderRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public bool Add(Order? order)
		{
			if (order != null)
			{
				_context.Orders.Add(order);
			}

			return Save();
		}

		public bool Delete(Order? order)
		{
			if (order != null)
			{
				_context.Orders.Remove(order);
			}

			return Save();
		}

		//public async Task<Order> GetByIdAndUserIdAsync(int? id, string userId)
		//{
		//	return await _context.Orders.Where(o => o.UserId.ToString() == userId).FirstOrDefaultAsync(p => p.Id == id);
		//}

		public async Task<IEnumerable<Order>> GetAll()
		{
			return await _context.Orders.ToListAsync();
		}
		public bool Save()
		{
			var save = _context.SaveChanges();
			return save > 0;
		}
	}
}
