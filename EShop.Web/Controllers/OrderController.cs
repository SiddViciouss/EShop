using EShop.Web.Code;
using EShop.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var test = HttpContext.Session.GetString("_test");
            var viewModel = new OrderViewModel(unitOfWork, HttpContext.Session);
            return View(viewModel);
        }


    }
}