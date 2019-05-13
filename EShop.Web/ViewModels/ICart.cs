using System.Collections.Generic;

namespace EShop.Web.ViewModels
{
    public interface ICart
    {
        void AddItem(CartItem item);
        void Clear();
        ICollection<CartItem> GetCartItems();
        int GetCartItemsCount();
    }
}