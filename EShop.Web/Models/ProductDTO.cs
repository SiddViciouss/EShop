using EShop.Web.Models.DbModels;
using System.ComponentModel.DataAnnotations;

namespace EShop.Web.Models
{
    public class ProductDTO
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [StringLength(200)]
        [Display(Name = "Тег")]
        public string Tag { get; set; }

        [StringLength(500)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Range(0, 999999)]
        [Display(Name = "Цена (руб.)")]
        public decimal Price { get; set; }

        public string PreviewImagePath { get; set; }
        public string ImagePaths { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
