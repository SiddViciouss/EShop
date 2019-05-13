using EShop.Web.Code;
using EShop.Web.Models;
using EShop.Web.Models.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Web.ViewModels
{
    public class OrderViewModel
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly Cart cart;
        protected readonly ICollection<CartItem> cartItems;

        public OrderViewModel(IUnitOfWork unitOfWork, ICart cart)
        {
            this.unitOfWork = unitOfWork;
            cartItems = cart.GetCartItems();
            var products = unitOfWork.Repository<Product>().Query().AsNoTracking().Where(p => cartItems.Any(c => c.ProductId == p.Id)).ToList();
            Items = new List<OrderItem>();
            foreach (var cartItem in cartItems)
            {
                var product = products.FirstOrDefault(p => p.Id == cartItem.ProductId);
                Items.Add(new OrderItem()
                {
                    Id = product.Id,
                    ProductName = product.Name,
                    Description = product.Description,
                    Count = cartItem.Count
                });
            }
        }

        public ICollection<OrderItem> Items { get; set; }
    }
}
