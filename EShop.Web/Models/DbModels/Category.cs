using EShop.Web.Data;
using System.Collections.Generic;

namespace EShop.Web.Models.DbModels
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }


        //public int? CategoryId { get; set; }

        //[ForeignKey("CategoryId")]
        //public Category Categories { get; set; }
        //public ICollection<Category> CategoriesList { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
