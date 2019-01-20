using EShop.Web.Data;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Web.Models.DbModels
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Tag { get; set; }
        //public int BrandId { get; set; }
        //public int UnitId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //public int Discount { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string PreviewImagePath { get; set; }
        public string ImagePaths { get; set; }

        [NotMapped]
        public bool IsNew
        {
            get
            {
                return (DateTime.Now - AddedDate).Days < 7;
            }
        }
        //public virtual Brand Brand { get; set; }
        //public virtual Unit Unit { get; set; }
        //public virtual ProductManual ProductManual { get; set; }
        //public virtual ICollection<ProductComments> ProductCommentses { get; set; }
        //public virtual ICollection<ProductImage> ProductImages { get; set; }
        //public virtual ICollection<ProductStock> ProductStocks { get; set; }
        //public virtual OrderDetails OrderDetails { get; set; }
    }
}
