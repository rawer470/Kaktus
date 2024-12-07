using System;
using AspNetCoreHero.ToastNotification.Abstractions;
using Kaktus.Models;
using Kaktus.NotifyClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kaktus.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager, INotyfService notifyService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            Notify.Configure(notifyService);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User? user = await userManager.FindByEmailAsync(details.EmailAddress);

                if (user != null)
                {
                    await signInManager.SignOutAsync();

                    var result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);

                    if (result.Succeeded)
                    {
                        Notify.ShowSuccess("Success",5);
                        return Redirect(returnUrl ?? "/");
                    }
                }

                ModelState.AddModelError(nameof(LoginModel.EmailAddress), "Invalid user or password");
                Notify.ShowError("Invalid user or password", 5);
            }

            return View(details);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { UserName = model.Name, Email = model.EmailAddress };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignOutAsync();

                    var res = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }


            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Quit()
        {
            signInManager.SignOutAsync();
            Notify.ShowWarning("Вы вышли из вашего аккаунта", 5);
            return RedirectToAction("Index", "Home");
        }
    }
}
