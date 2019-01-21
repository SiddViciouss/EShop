using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EShop.Web.Code;
using EShop.Web.Models;
using EShop.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IUnitOfWork unitOfWork,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.unitOfWork = unitOfWork;
            this.signInManager = signInManager;
        }
        
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Authorize]
        public IActionResult Profile()
        {
            ApplicationUser user = userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name || x.Email == User.Identity.Name);
            return View(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (model.Email.IndexOf('@') > -1)
            {
                //Validate email format
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(model.Email))
                {
                    ModelState.AddModelError("Email", "Email is not valid");
                }
            }
            else
            {
                //validate Username format
                string emailRegex = @"^[a-zA-Z0-9]*$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(model.Email))
                {
                    ModelState.AddModelError("Email", "Username is not valid");
                }
            }

            if (ModelState.IsValid)
            {
                var userName = model.Email;
                if (userName.IndexOf('@') > -1)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                    userName = user.UserName;

                }
                var result = await signInManager.PasswordSignInAsync(userName, model.Password, model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    ApplicationUser user = userManager.Users.FirstOrDefault(x => x.UserName == userName || x.Email == userName);

                    if (user != null)
                    {
                        if (await userManager.IsInRoleAsync(user, "Admin"))
                        {
                            if (returnUrl == null)
                            {
                                return RedirectToAction("Index", "Product");
                            }
                            return RedirectToLocal(returnUrl);
                        }
                        if (await userManager.IsInRoleAsync(user, "User"))
                        {
                            return RedirectToLocal(returnUrl);
                        }
                    }

                }
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(ProductController.Index), "Home");
            }
        }
    }
}