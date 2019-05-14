using EShop.Web.Data;
using EShop.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Web.Code
{
    public class IdentityDbInitializer
    {
        public static ApplicationDbContext _context;

        public IdentityDbInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public static void SeedData(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            //int cityId = _context.Cities.First(x => x.Name == "Dhaka").Id;
            if (userManager.FindByNameAsync
                    ("user1@localhost.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "user1@localhost.com",
                    Email = "user1@localhost.com",
                    Name = "Простой пользователь",
                    DateOfBirth = new DateTime(1960, 1, 1),
                    PhoneNumber = "+7(3422)44-55-77",
                    Gender = -1,
                    City = "Пермь",
                    Street = "ул. Ленина", 
                    Building  = "10",
                    FlatNumber = 123,
                    AvailableMoney = 5000
                };

                IdentityResult result = userManager.CreateAsync
                    (user, "12345678").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "User").Wait();
                }
            }


            if (userManager.FindByNameAsync
                    ("admin@localhost.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@localhost.com",
                    Email = "admin@localhost.com",
                    Name = "Mr Admin",
                    DateOfBirth = new DateTime(1985, 1, 1),
                    Gender = 0,
                    PhoneNumber = "+7-900-333-44-44",
                    City = "Пермь",
                    Street = "ул. Ленина",
                    Building = "10",
                    FlatNumber = 123,
                    AvailableMoney = 500000
                    //CityId = cityId
                };

                IdentityResult result = userManager.CreateAsync
                    (user, "12345678").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Admin").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
                ("User").Result)
            {
                ApplicationRole role = new ApplicationRole
                {
                    Name = "User",
                    Description = "Perform normal operations."
                };
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync
                ("Admin").Result)
            {
                ApplicationRole role = new ApplicationRole
                {
                    Name = "Admin",
                    Description = "Perform all the operations."
                };
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                ("Editor").Result)
            {
                ApplicationRole role = new ApplicationRole
                {
                    Name = "Editor",
                    Description = "Can Edit  all the operations."
                };
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }
        }
    }
}
