using EShop.Web.Code;
using EShop.Web.Models;
using EShop.Web.Models.DbModels;
using EShop.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var model = new ShippingModel();
            if (User.Identity.IsAuthenticated)
            {
                var user = userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name || x.Email == User.Identity.Name);
                model.CustomerName = user.Name;
                model.PhoneNumber = user.PhoneNumber;
                model.Email = user.Email;
                model.UserId = user.Id;
            }
            else
            {
                model.AvailableMoney = 5000;
            }
            var cartItems = cart.GetCartItems();
            var productList = unitOfWork.Repository<Product>().Query().AsNoTracking().Where(x => cartItems.Any(item => item.ProductId == x.Id)).ToList();
            var sum = 0m;
            foreach (var cartItem in cartItems)
            {
                var product = productList.FirstOrDefault(x => x.Id == cartItem.ProductId);
                sum += cartItem.Count * product.Price;
            }
            model.PriceTotal = sum;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Shipping(ShippingModel model)
        {
            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                cart.Clear();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(model);
        }
    }
}