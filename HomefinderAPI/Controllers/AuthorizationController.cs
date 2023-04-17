using System.Security.Claims;
using HomefinderAPI.ViewModels.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomefinderAPI.Controllers
{
  [ApiController]
		[Route("api/auth")]
		public class AuthorizationController : ControllerBase
		{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
		
			public AuthorizationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
			{
      _signInManager = signInManager;
      _userManager = userManager;
					
			}

			[HttpPost("register")]
			public async Task<ActionResult<UserViewModel>> Register(RegisterUserViewModel model)
			{
				var user = new IdentityUser
				{
					Email = model.Email!.ToLower(),
					UserName = model.Email.ToLower()
				};

				var result = await _userManager.CreateAsync(user, model.Password!);

				if (result.Succeeded)
				{
					if (model.IsAdmin)
					{
						await _userManager.AddClaimAsync(user, new Claim("Admin", "true"));
					}

					var userData = new UserViewModel
					{
						Username = user.UserName
					};

					return StatusCode(201, userData);
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("User registration", error.Description);
					}
					return StatusCode(500, ModelState);
				}
			}

			[HttpPost("login")]
			public async Task<ActionResult<UserViewModel>> Login(LoginUserViewModel model)
			{
				var user = await _userManager.FindByNameAsync(model.UserName!);

				if (user is null)
				{
					return Unauthorized("Felaktigt användarnamn");
				}

				var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password!, true);

				if (!result.Succeeded)
				{
					return Unauthorized(result.IsLockedOut ? "Kontot är låst i 10 minuter på grund av för många försök":"Felaktigt lösenord");
				}

				var userData = new UserViewModel
				{
					Username = user.UserName
				};

				return Ok(userData);
			}
		}
}