using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationLabWork9.Data;
using WebApplicationLabWork9.Models;
using WebApplicationLabWork9.ViewModels;

namespace WebApplicationLabWork9.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _singInManager;
		private readonly ApplicationDbContext _context;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> singInManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_singInManager = singInManager;
			_context = context;
		}

		public IActionResult Login()
		{
			var response = new LoginViewModel();
			return View(response);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid) return View(loginViewModel);

			var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

			if (user != null)
			{
				// User is found, checking password
				var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
				if (passwordCheck)
				{
					//Password is correct, signing in
					var result = await _singInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}
				}
				//Password is false
				TempData["Error"] = "Неправильний пароль. Спробуйте ще";
				return View(loginViewModel);
			}
			// User not found
			TempData["Error"] = "Неправильний логін. Спробуйте ще";
			return View(loginViewModel);
		}

		[HttpGet]
        public IActionResult Registration()
        {
            var response = new RegistrationViewModel();
            return View(response);
        }

		[HttpPost]
		public async Task<IActionResult> Registration(RegistrationViewModel registrationViewModel)
		{
			if (!ModelState.IsValid) return View(registrationViewModel);

			var user = await _userManager.FindByEmailAsync(registrationViewModel.Email);
			if (user != null)
			{
				TempData["Error"] = "Цей емейл вже зайнятий";
				return View(registrationViewModel);
			}

			var newUser = new AppUser()
			{
				Email = registrationViewModel.Email,
				UserName = registrationViewModel.UserName,
			};
			var newUserResponse = await _userManager.CreateAsync(newUser, registrationViewModel.Password);

			if (newUserResponse.Succeeded)
			{
				await _userManager.AddToRoleAsync(newUser, UserRoles.User);
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _singInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

        [HttpGet]
        public async Task<IActionResult> EditUser()
        {
			var user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				return NotFound();
			}


			var model = new EditUserViewModel
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Gender = user.Gender,
				Age = user.Age,
			};

			return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
			var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
			{
				return NotFound(model.Email);
			}
			else
			{
				user.Email = model.Email;
				user.Gender = model.Gender;
				user.Age = model.Age;
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;
			}

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Home");
			}

			return View(model);
        }
    }
}
