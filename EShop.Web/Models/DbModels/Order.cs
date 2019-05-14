using EShop.Web.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Web.Models.DbModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ProductList> ProductList { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }

    public enum OrderStatus
    {
        Created = 0,
        PaidFor = 1,
        Shipped = 2,
        Deleted = 3
        //Confirmed = 4,
    }
}
