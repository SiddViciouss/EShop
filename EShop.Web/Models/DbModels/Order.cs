using EShop.Web.Data;

namespace EShop.Web.Models.DbModels
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
    }

    public enum OrderStatus
    {
        Created = 0,
        Confirmed = 1,
        PaidFor = 2,
        Deleted = 3
    }
}
