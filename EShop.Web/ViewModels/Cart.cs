using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Web.ViewModels
{
    public class Cart
    {
        protected readonly string sessionId = "_cart";
        protected ISession session;

        public Cart(ISession session)
        {
            this.session = session;
        }

        public void AddItem(int productId, int count)
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
            var existingItem = cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (existingItem == null)
            {
                cartItems.Add(new CartItem() { ProductId = productId, Count = count });
            }
            else
            {
                existingItem.Count += count;
            }
            session.SetString(sessionId, JsonConvert.SerializeObject(cartItems));
        }

        public void Clear()
        {
            session.SetString(sessionId, string.Empty);
        }
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
