using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class CartController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult AddItem(int ProductId, int Count)
        {
            var cart = new Cart(HttpContext.Session);
            cart.AddItem(new CartItem() { ProductId = ProductId, Count = Count});
            return Ok();
        }
    }
}