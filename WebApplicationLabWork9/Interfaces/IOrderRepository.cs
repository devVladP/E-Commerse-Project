using WebApplicationLabWork9.Models;

namespace WebApplicationLabWork9.Interfaces
{
	public interface IOrderRepository
	{
		Task<IEnumerable<Order>> GetAll();
		//Task<Order> GetByIdAndUserIdAsync(int? id, string userId);
		bool Add(Order? order);
		bool Delete(Order? order);
		bool Save();
	}
}
