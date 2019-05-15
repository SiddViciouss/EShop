using EShop.Web.Code;
using EShop.Web.Models;
using EShop.Web.Models.DbModels;
using EShop.Web.Services;
using EShop.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            //return Redirect(Url.Action("Index", "Order"));
            return RedirectToAction(actionName: nameof(Index));
        }

        public IActionResult RemoveFromCart(int productId)
        {
            cart.RemoveItem(productId);
            return RedirectToAction(actionName: nameof(Index));
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
                model.City = user.City;
                model.Street = user.Street;
                model.Building = user.Building;
                model.FlatNumber = user.FlatNumber;
                model.AvailableMoney = user.AvailableMoney;
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
        public async Task<IActionResult> Shipping(ShippingModel model)
        {
            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                // register user if necessary
                if (string.IsNullOrEmpty(model.UserId))
                {
                    var userCheck = await userManager.FindByNameAsync(model.Email);
                    if (userCheck != null)
                    {
                        ModelState.AddModelError("Email", "Пользователь с таким Email уже зарегистрирован");
                        return View(model);
                    }
                    var newUser = new ApplicationUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = model.CustomerName,
                        PhoneNumber = model.PhoneNumber,
                        PhoneNumberConfirmed = true, // mock
                        Email = model.Email,
                        UserName = model.Email,
                        City = model.City,
                        Street = model.Street,
                        Building = model.Building,
                        FlatNumber = model.FlatNumber,
                        AvailableMoney = 5000,
                        EmailConfirmed = true // mock
                    };
                    var pwd = Utility.GenerateRandomPassword();
                    var createUserResult = await userManager.CreateAsync(newUser, pwd);
                    if (createUserResult.Succeeded)
                    {
                        createUserResult = await userManager.AddToRoleAsync(newUser, "User");
                    }
                    if (!createUserResult.Succeeded)
                    {
                        throw new Exception($"Unexpected error occured on user register: {createUserResult.Errors.FirstOrDefault()?.Description}");
                    }
                    var emailService = new EmailService();
                    // sending email with registration information
                    try
                    {
                        await emailService.SendEmailAsync(newUser.Email, "Регистрация на сайте 42studio.org", $"Регистрация на сайте 42studio.org прошла успешно. Ваш логин: {newUser.UserName} , пароль: {pwd}");
                    }
                    catch(Exception ex)
                    {
                        //ToDo: log exception
                    }
                    model.UserId = newUser.Id;
                }
                // creating order
                Order orderDb = null;
                try
                {
                    var orderRecord = new Order()
                    {
                        UserId = model.UserId,
                        AddedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        OrderStatus = OrderStatus.Created
                    };
                    orderDb = await unitOfWork.Repository<Order>().InsertAsync(orderRecord);
                }
                catch (Exception ex)
                {
                    //ToDo: log exception
                    throw new DbUpdateException("Unexpected error occured on order create, see InnerException for details", ex);
                }
                var cartItems = cart.GetCartItems();
                try
                {
                    foreach (var item in cartItems)
                    {
                        var listRecord = new ProductList()
                        {
                            OrderId = orderDb.Id,
                            ProductId = item.ProductId,
                            Count = item.Count
                        };
                        await unitOfWork.Repository<ProductList>().InsertAsync(listRecord);
                    }
                }
                catch (Exception ex)
                {
                    //ToDo: log exception
                    throw new DbUpdateException("Unexpected error occured on product list create, see InnerException for details", ex);
                }
                // substracting user money
                var user = await userManager.FindByIdAsync(model.UserId);
                user.AvailableMoney -= model.PriceTotal;
                var substractResult = await userManager.UpdateAsync(user);
                if (substractResult.Succeeded)
                {
                    orderDb.OrderStatus = OrderStatus.PaidFor;
                    await unitOfWork.Repository<Order>().UpdateAsync(orderDb);
                    cart.Clear();
                }
                return RedirectToAction(actionName: nameof(Completed));
            }
            return View(model);
        }

        public IActionResult Completed() => View();
    }
}