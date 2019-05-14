using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Web.ViewModels
{
    public class Cart : ICart
    {
        protected readonly string sessionId = "_cart";
        protected ISession session;

        public Cart(IHttpContextAccessor httpContextAccessor)
        {
            session = httpContextAccessor.HttpContext.Session;
        }

        public ICollection<CartItem> GetCartItems()
        {
            ICollection<CartItem> cartItems = null;
            var itemsText = session.GetString(sessionId);
            if (string.IsNullOrEmpty(itemsText))
            {
                cartItems = new List<CartItem>();
            }
            else
            {
                cartItems = JsonConvert.DeserializeObject<ICollection<CartItem>>(itemsText);
            }
            return cartItems;
        }

        public void AddItem(CartItem item)
        {
            var cartItems = GetCartItems();
            var existingItem = cartItems.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existingItem == null)
            {
                cartItems.Add(new CartItem() { ProductId = item.ProductId, Count = item.Count });
            }
            else
            {
                existingItem.Count += item.Count;
            }
            session.SetString(sessionId, JsonConvert.SerializeObject(cartItems));
        }

        public void RemoveItem(int productId)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (item == null)
            {
                throw new ArgumentException("Cart item for delete not found.");
            }
            cartItems.Remove(item);
            session.SetString(sessionId, JsonConvert.SerializeObject(cartItems));
        }

        public void Clear()
        {
            session.SetString(sessionId, string.Empty);
        }

        public int GetCartItemsCount()
        {
            return GetCartItems().Count;
        }
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
