using EShop.Web.Code;
using EShop.Web.ViewModels;
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

        public IActionResult Index()
        {
            var viewModel = new ProductViewModel(unitOfWork, null);
            return View(viewModel);
        }
    }
}