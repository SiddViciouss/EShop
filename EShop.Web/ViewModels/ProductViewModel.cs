using EShop.Web.Code;
using EShop.Web.Models.DbModels;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Web.ViewModels
{
    public class ProductViewModel
    {
        private readonly IUnitOfWork unitOfWork;
        public List<Product> Products { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public List<Category> AvailableCategories { get; set; }
        public ProductViewModel(IUnitOfWork unitOfWork, int? categoryId, int pageSize = 15, int currentPage = 1)
        {
            this.unitOfWork = unitOfWork;
            PageSize = pageSize;
            CurrentPage = currentPage;
            var productRepo = unitOfWork.Repository<Product>();
            if (categoryId.HasValue)
            {
                Products = productRepo.Query()
                            .Where(x => x.CategoryId == categoryId.Value)
                            .Skip((CurrentPage - 1) * PageSize)
                            .Take(PageSize)
                            .ToList();
            }
            else
            {
                Products = productRepo
                            .Query()
                            .Skip((CurrentPage - 1) * PageSize)
                            .Take(PageSize)
                            .ToList();
            }
            AvailableCategories = this.unitOfWork.Repository<Category>().GetAll().ToList();
        }

    }
}
