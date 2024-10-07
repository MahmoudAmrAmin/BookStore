using Library_Managemnt_System.Models;
using Library_Managemnt_System.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library_Managemnt_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController
            (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
		[HttpPost]//<a href>
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterUser newUserVM)
		{
			if (ModelState.IsValid)
			{
				//create account
				ApplicationUser userModel = new ApplicationUser();
				userModel.UserName = newUserVM.UserName;
				userModel.PasswordHash = newUserVM.Password;
				userModel.PhoneNumber = newUserVM.PhoneNumber;
                userModel.Email = newUserVM.Email;
				IdentityResult result = await userManager.CreateAsync(userModel, newUserVM.Password);
				if (result.Succeeded == true)
				{
					//creat cookie
					await signInManager.SignInAsync(userModel, false);
					return View("Login");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}

			}
			return View(newUserVM);
		}

		public IActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]//requets.form['_requetss]
        public async Task<IActionResult> SaveLogin(LoginUser userViewModel)
        {
            if (ModelState.IsValid == true)
            {
                //check found 
                ApplicationUser appUser =
                    await userManager.FindByNameAsync(userViewModel.Name);
                if (appUser != null)
                {
                    bool found =
                         await userManager.CheckPasswordAsync(appUser, userViewModel.Password);
                    if (found == true)
                    {
                        List<Claim> Claims = new List<Claim>();                      

                        await signInManager.SignInWithClaimsAsync(appUser, userViewModel.RememberMe, Claims);
                        //await signInManager.SignInAsync(appUser, userViewModel.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }

                }
                ModelState.AddModelError("", "Username OR PAssword wrong");
                //create cookie
            }
            return View("Login", userViewModel);
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return View("Login");
        }
    }

}
