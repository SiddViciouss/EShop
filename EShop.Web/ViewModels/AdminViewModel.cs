using EShop.Web.Code;
using EShop.Web.Models;
using EShop.Web.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Web.ViewModels
{
    public class AdminViewModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminViewModel(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            ProductList = this.unitOfWork.Repository<Product>()
                                .GetAllInclude(x => x.Category)
                                .Select(x => new ProductDTO()
                                {
                                    Id = x.Id,
                                    Code = x.Code,
                                    CategoryId = x.CategoryId,
                                    Category = x.Category,
                                    Description = x.Description,
                                    ImagePaths = x.ImagePaths,
                                    Name = x.Name,
                                    PreviewImagePath = x.PreviewImagePath,
                                    Price = x.Price,
                                    Tag = x.Tag
                                })
                                .ToList();
            this.userManager = userManager;
            UserList = userManager.Users
                        .Select(u => new UserDTO()
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                            Name = u.Name,
                            Gender = u.Gender,
                            DateOfBirth = u.DateOfBirth,
                            JoinIp = u.JoinIp,
                            PhoneNumber = u.PhoneNumber,
                            City = u.City,
                            Street = u.Street,
                            Building = u.Building,
                            FlatNumber = u.FlatNumber,
                            AvailableMoney = u.AvailableMoney,
                        })
                        .ToList();
        }

        public ICollection<ProductDTO> ProductList { get; set; }
        public ICollection<UserDTO> UserList { get; set; }
    }
}
