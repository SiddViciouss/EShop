using EShop.Web.Code;
using EShop.Web.Models;
using EShop.Web.Models.DbModels;
using EShop.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            //var viewModel = unitOfWork.Repository<Product>().Query().Include(x => x.Category).ToList();
            var viewModel = new AdminViewModel(unitOfWork, userManager);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProductSave(ProductDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserSave(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return BadRequest();
            }
            user.Name = model.Name;
            //user.Gender = model.Gender;
            //user.DateOfBirth = model.DateOfBirth;
            user.PhoneNumber = model.PhoneNumber;
            user.City = model.City;
            user.Street = model.Street;
            user.Building = model.Building;
            user.FlatNumber = model.FlatNumber;
            user.AvailableMoney = model.AvailableMoney;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UserDelete(string userId)
        {
            var userOrders = await unitOfWork.Repository<Order>().FindAllAsync(x => x.UserId == userId);
            if (userOrders != null && userOrders.Count > 0)
            {
                var lists = unitOfWork.Repository<ProductList>().FindAll(x => userOrders.Any(y => y.Id == x.OrderId));
                foreach (var list in lists)
                {
                    await unitOfWork.Repository<ProductList>().DeleteAsync(list);
                }
                foreach (var order in userOrders)
                {
                    await unitOfWork.Repository<Order>().DeleteAsync(order);
                }
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            var res = await userManager.DeleteAsync(user);
            if (!res.Succeeded)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index)); ;
        }

        public async Task<IActionResult> UserFormPartial(string userId)
        {
            UserDTO model = new UserDTO();
            if (!string.IsNullOrEmpty(userId))
            {
                var appUser = await userManager.FindByIdAsync(userId);
                model.Id = userId;
                model.Name = appUser.Name;
                model.UserName = appUser.UserName;
                model.Email = appUser.Email;
                model.PhoneNumber = appUser.PhoneNumber;
                model.City = appUser.City;
                model.Street = appUser.Street;
                model.Building = appUser.Building;
                model.FlatNumber = appUser.FlatNumber;
                model.AvailableMoney = appUser.AvailableMoney;
            }

            return PartialView("_UserFormPartial", model);
        }

        public IActionResult ProductFormPartial(int? productId)
        {
            var model = new ProductDTO();
            if (productId.HasValue && productId >= 0)
            {
                var product = unitOfWork.Repository<Product>().Query().AsNoTracking().FirstOrDefault(x => x.Id == productId); ///.FindAsync(p => p.Id == productId);
                if (product != null)
                {
                    model.Id = productId.Value;
                    model.Code = product.Code;
                    model.ImagePaths = product.ImagePaths;
                    model.PreviewImagePath = product.PreviewImagePath;
                    model.Name = product.Name;
                    model.Price = product.Price;
                    model.Tag = product.Tag;
                    model.CategoryId = product.CategoryId;
                    model.Category = product.Category;
                    model.Description = product.Description;
                }
                //model.Id = 1;
                //model.Code = "K001";
                //model.ImagePaths = "";
                //model.PreviewImagePath = "";
                //model.Name = "Name";
                //model.Price = 111;
                //model.Tag = "product.Tag";
                //model.CategoryId = 1;
                //model.Category = null;
                //model.Description = "product.Description";
            }
            return PartialView("_ProductFormPartial", model);
        }
    }
}