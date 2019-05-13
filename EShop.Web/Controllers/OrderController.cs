using EShop.Web.Code;
using EShop.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICart cart;

        public OrderController(IUnitOfWork unitOfWork, ICart cart)
        {
            this.unitOfWork = unitOfWork;
            this.cart = cart;
        }

        public IActionResult Index()
        {
            //var test = HttpContext.Session.GetString("_test");
            var viewModel = new OrderViewModel(unitOfWork, cart);
            return View(viewModel);
        }
    }
}