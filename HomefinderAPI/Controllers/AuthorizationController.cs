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
		
			public AuthorizationController(UserManager<IdentityUser> userManager)
			{
      _userManager = userManager;
					
			}

			[HttpPost("register")]
			public async Task<ActionResult<UserViewModel>> RegisteruserAsync(RegisterUserViewModel model)
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
		}
}