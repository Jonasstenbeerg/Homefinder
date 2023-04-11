using HomefinderAPI.Controllers;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace HomefinderAPI.Tests.Controllers
{
  [TestClass]
    public class AuthorizationControllerTest
    {
       private AuthorizationController? _controller;
		private IdentityUser? _mockedIdentityUser;

		[TestInitialize]
		public void TestInitialize()
		{
			var mockedSignInManager = new Mock<SignInManager<IdentityUser>>();
            var mockedUserManager = new Mock<UserManager<IdentityUser>>();
			_mockedIdentityUser = new IdentityUser { UserName = "TestUser" };
			_controller = new AuthorizationController(mockedUserManager.Object,mockedSignInManager.Object);
		}
    }
}