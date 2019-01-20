using EShop.Web.Data;
using EShop.Web.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Web.Code
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Categories.Any())
            {
                CreateCategories(context);
            }
            if (!context.Products.Any())
            {
                CreateProducts(context);
            }
        }

        public static void CreateCategories(ApplicationDbContext context)
        {
            var categories = new List<Category>()
            {
                new Category() { Id = 1, Name = "Кольца", Description = "Кольца", AddedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new Category() { Id = 2, Name = "Серьги", Description = "Серьги", AddedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new Category() { Id = 3, Name = "Кулоны", Description = "Кулоны", AddedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new Category() { Id = 4, Name = "Броши", Description = "Броши", AddedDate = DateTime.Now, ModifiedDate = DateTime.Now }
            };
            try
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //ToDo: log exception
            }
        }

        public static void CreateProducts(ApplicationDbContext context)
        {
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    CategoryId = 1,
                    Code = "K0001",
                    Name = "Кулон \"Geometric tile\"",
                    Description = "Длина - 27мм(полная)\\n Ширина - 27мм(широчайшая часть)\\n Толщина - 5мм(самое толстое место) Абаш термический, ювелирная смола, фурнитура сталь. Финиш натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 600,
                    Tag = "Кольца",
                    PreviewImagePath = "~/upload/1.jpg",
                    ImagePaths = "~/upload/1.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product()
                {
                    Id = 2,
                    CategoryId = 1,
                    Code = "K0002",
                    Name = "Кольцо \"Anthill of lava\"",
                    Description = "Размер 18. Дерево кедр, ювелирная смола.Финиш натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 1200,
                    Tag = "Кольца",
                    PreviewImagePath = "~/upload/2.jpg",
                    ImagePaths = "~/upload/2.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }//,
                //new Product()
                //{
                //    Id = 1,
                //    CategoryId = 0,
                //    Code = "K0001",
                //    Name = "",
                //    Description = "",
                //    Price = 100,
                //    Tag = "",
                //    AddedDate = DateTime.Now,
                //    ModifiedDate = DateTime.Now
                //}
            };
            try
            {
                context.Products.AddRange(products);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //ToDo: log exception
            }
        }
    }
}
