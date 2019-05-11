using EShop.Web.Code;
using EShop.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? categoryId = null)
        {
            var viewModel = new ProductViewModel(unitOfWork, categoryId);
            //HttpContext.Session.SetString("_test", "test_test");
            return View(viewModel);
        }

        public IActionResult Details(int productId)
        {
            return View();
        }
    }
}