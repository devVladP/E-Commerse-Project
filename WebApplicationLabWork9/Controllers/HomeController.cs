using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLabWork9.Interfaces;
using WebApplicationLabWork9.Models;
using WebApplicationLabWork9.ViewModels;
using WebApplicationLabWork9.Data;

namespace WebApplicationLabWork9.Controllers
{
	public class HomeController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IProductRepository _productRepository;
		private readonly IOrderRepository _orderRepository;
		public HomeController(IProductRepository productRepository, IOrderRepository orderRepository, UserManager<AppUser> userManager)
		{
			this._productRepository = productRepository;
			this._orderRepository = orderRepository;
			this._userManager = userManager;
		}

		public async Task<IActionResult> Index(string? categories, string searchString, int pageNumber = 1)
		{
			var productFiltered = await _productRepository.GetAll();
			string userId = "123";
			if (User.Identity.IsAuthenticated)
			{
				var user = await _userManager.GetUserAsync(User);
				userId = await _userManager.GetUserIdAsync(user);
			}
			if (searchString != null)
			{
				pageNumber = 1;
			}
			if (!string.IsNullOrEmpty(categories))
			{
				productFiltered = productFiltered.Where(p => p.Category.ToUpper().Contains(categories.ToUpper()));
			}
			if (!string.IsNullOrEmpty(searchString))
			{
				productFiltered = productFiltered.Where(p => p.Name.ToUpper().Contains(searchString.ToUpper()));
			}

			int pageSize = 4;
			IEnumerable<Product> productsPerPage =  productFiltered.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			PaginatedList paginatedList = new PaginatedList { PageNumber = pageNumber, PageSize = pageSize, TotalItems = productFiltered.Count() };
			IndexViewModel i = new IndexViewModel(paginatedList, productsPerPage, userId);
			return View(i);
		}

		public async Task<IActionResult> ShoppingCart()
		{
			string userId = "123";
			if (User.Identity.IsAuthenticated)
			{
				var user =  await _userManager.GetUserAsync(User);
				userId = await _userManager.GetUserIdAsync(user);
			}

			decimal totalsum = 0;
			var productsAll = await _productRepository.GetAll();
			var ordersAll = await _orderRepository.GetAll();
			totalsum = ordersAll.Where(o => o.UserId.ToString() == userId)
				.Join(productsAll,
									o => o.ProductId,
									p => p.Id,
									(o, p) => new { ProductId = p.Id, Price = p.Price })
									.Sum(p => p.Price);

			ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel()
			{
				products = productsAll,
				orders = ordersAll,
				UserId = userId,
				totalsum = totalsum,
			};

			return View(shoppingCartViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(Order order)
		{
			_orderRepository.Add(order);
			if (_orderRepository.Save())
			{
			}

			return RedirectToAction("ShoppingCart");
		}

		[HttpPost]
		public async Task<IActionResult> Plus(int? id, string userId)
		{
			if (id != null)
			{
				Order orderToAdd = new Order { ProductId = (int)id, UserId = new Guid(userId) };
				_orderRepository.Add(orderToAdd);
				if (_orderRepository.Save())
				{
				}
			}

			return RedirectToAction("ShoppingCart");
		}

		[HttpPost]
		public async Task<IActionResult> Minus(int? id, string userId)
		{
			if (id != null)
			{
				var orders = await _orderRepository.GetAll();
				Order orderToDelete = orders.First(o => o.ProductId == id && o.UserId.ToString() == userId);
				_orderRepository.Delete(orderToDelete);

				if (_orderRepository.Save())
				{
				}

			}

			return RedirectToAction("ShoppingCart");
		}



		[HttpPost]
		public async Task<IActionResult> Delete(int? id, string userId)
		{
			if (id != null) 
			{
				var orders = await _orderRepository.GetAll();
				IEnumerable<Order> ordersToDelete = orders.Where(o => o.ProductId == id && o.UserId.ToString() == userId);

				foreach(var order in ordersToDelete)
				{
					_orderRepository.Delete(order);
				}

				if (_orderRepository.Save())
				{
				}

			}

			return RedirectToAction("ShoppingCart");
		}

		public IActionResult CreateProduct()
		{
			var response = new CreateProductViewModel();
			return View(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductViewModel createProductViewModel)
		{
			if (!ModelState.IsValid) return View(createProductViewModel);

			var products = await _productRepository.GetAll();

			Product? p = products.FirstOrDefault(product => product.Name.ToUpper() == createProductViewModel.Name.ToUpper());
			if (p is not null)
			{
				TempData["Error"] = "Цей продукт вже існує";
				return View(createProductViewModel);
			}

			var newProduct = new Product()
			{
				Name = createProductViewModel.Name,
				Price = createProductViewModel.Price,
				Image = createProductViewModel.Image,
			};

			_productRepository.Add(newProduct);

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> ClearOrders()
		{
			var orders = await _orderRepository.GetAll();
			foreach(var order in orders)
			{
				_orderRepository.Delete(order);
			}

			if (_orderRepository.Save())
			{
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> EditProduct(int? id)
		{
			if (id != null)
			{
				Product? product = await _productRepository.GetByIdAsync(id);
				if (product != null) return View(product);
			}

			return NotFound();
		}

		[HttpPost]
		public IActionResult EditProduct(Product product)
		{
			_productRepository.Update(product);
			if (_productRepository.Save())
			{
			}
			return RedirectToAction("Index");
		}

		public IActionResult HomePage()
		{
			return View();
		}

		//public async Task<IActionResult> Minus(int? id)
		//{
		//	if (id != null)
		//	{
		//		Product? p = await _productRepository.GetByIdAsync((int)id);
		//		if (p != null)
		//		{
		//			if (p.Quantity > 1)
		//			{
		//				p.Quantity--;
		//				_productRepository.Update(p);
		//				if (_productRepository.Save())
		//				{
		//				}
		//			}
		//			else if (p.Quantity == 1)
		//			{
		//				p.Quantity--;
		//				_productRepository.Delete(p);
		//				if (_productRepository.Save())
		//				{
		//				}
		//			}
		//		}
		//	}

		//	return RedirectToAction("ShoppingCart");
		//}

		//public async Task<IActionResult> Plus(int? id)
		//{
		//	if (id != null)
		//	{
		//		Product? p = await _productRepository.GetByIdAsync((int)id);
		//		p.Quantity++;
		//		_productRepository.Update(p);
		//		if (_productRepository.Save())
		//		{
		//		}
		//	}
		//	return RedirectToAction("ShoppingCart");
		//}
	}
}
