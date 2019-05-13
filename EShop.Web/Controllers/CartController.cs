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
        private ICart cart;

        public CartController(ICart cart)
        {
            this.cart = cart;
        }

        [HttpPost]
        public ActionResult AddItem(int ProductId, int Count)
        {
            cart.AddItem(new CartItem() { ProductId = ProductId, Count = Count});
            return Ok();
        }
    }
}