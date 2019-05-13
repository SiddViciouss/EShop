using EShop.Web.Code;
using EShop.Web.Models;
using EShop.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICart cart;
        private readonly UserManager<ApplicationUser> userManager;

        public OrderController(IUnitOfWork unitOfWork, ICart cart, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.cart = cart;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            //var test = HttpContext.Session.GetString("_test");
            var viewModel = new OrderViewModel(unitOfWork, cart);
            return View(viewModel);
        }

        public IActionResult ClearCart()
        {
            cart.Clear();
            return Redirect(Url.Action("Index", "Order"));
        }

        public IActionResult Shipping()
        {
            return View();
        }
    }
}