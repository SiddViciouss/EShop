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
            //context.Database.EnsureCreated();
            context.Database.Migrate();
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
                    PreviewImagePath = "/upload/1.jpg",
                    ImagePaths = "/upload/1.jpg",
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
                    PreviewImagePath = "/upload/2.jpg",
                    ImagePaths = "/upload/2.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product()
                {
                    Id = 3,
                    CategoryId = 1,
                    Code = "K0003",
                    Name = "Кольцо \"Chipped lava\"",
                    Description = "Размер 17,5. Дерево дуб шпон в 4 слоя, ювелирная смола, Люминесцент - бирюзовый(светится в темноте). Финиш натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 1600,
                    Tag = "Кольца",
                    PreviewImagePath = "/upload/3.jpg",
                    ImagePaths = "/upload/3.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product()
                {
                    Id = 4,
                    CategoryId = 1,
                    Code = "K0004",
                    Name = "Кольцо \"Ice block in the blizzards\"",
                    Description = "Размер 17. Абаш термический, ювелирная смола, необработанное стекло с подкраской, Люминесцент - синий(светится в темноте).Финиш натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 2500,
                    Tag = "Кольца",
                    PreviewImagePath = "/upload/4.jpg",
                    ImagePaths = "/upload/4.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product()
                {
                    Id = 5,
                    CategoryId = 2,
                    Code = "С0001",
                    Name = "Серьги \"Little violet glow\"",
                    Description = "Длина - 61мм(полная) Ширина - 8мм(лицевая часть) Абаш термический, ювелирная смола, фурнитура сталь, Люминесцент-зелёный(светится в темноте). Финиш натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 1800,
                    Tag = "Серьги",
                    PreviewImagePath = "/upload/5.jpg",
                    ImagePaths = "/upload/5.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product()
                {
                    Id = 6,
                    CategoryId = 1,
                    Code = "K0005",
                    Name = "Кольцо \"Icy gloom\"",
                    Description = "Размер 16. Абаш термический, ювелирная смола. Финиш натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 1550,
                    Tag = "Кольца",
                    PreviewImagePath = "/upload/6.jpg",
                    ImagePaths = "/upload/6.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product()
                {
                    Id = 7,
                    CategoryId = 2,
                    Code = "С0002",
                    Name = "Серьги \"Half balance\"",
                    Description = "Длина - 38мм(полная) Ширина - 6мм(лицевая часть) Абаш светлый, абаш термический, ювелирная смола, фурнитура сталь, Люминесцент-зелёный(светится в темноте). Финиш морилкой и натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 1300,
                    Tag = "Серьги",
                    PreviewImagePath = "/upload/7.jpg",
                    ImagePaths = "/upload/7.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product()
                {
                    Id = 8,
                    CategoryId = 4,
                    Code = "Б0001",
                    Name = "Брошь \"Strata guitar\"",
                    Description = "Длина - 82мм(полная) Ширина - 31мм(широкая часть деки) Толщина - 8мм(дека) Абаш светлый, ювелирная смола, частично пигмент с блестками, фурнитура сталь. Финиш натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 1300,
                    Tag = "Броши",
                    PreviewImagePath = "/upload/8.jpg",
                    ImagePaths = "/upload/8.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product()
                {
                    Id = 9,
                    CategoryId = 1,
                    Code = "K0006",
                    Name = "Кольцо \"Incorporeal\"",
                    Description = "Размер 17,5. Дерево кедра, ювелирная смола. Финиш морилкой, натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 950,
                    Tag = "Кольца",
                    PreviewImagePath = "/upload/9.jpg",
                    ImagePaths = "/upload/9.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product()
                {
                    Id = 10,
                    CategoryId = 1,
                    Code = "K0007",
                    Name = "Кольцо \"Acrylic riot\"",
                    Description = "Размер 17. Дерево дуб, ювелирная смола, Включения из белого и черного акрила. Финиш натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 1500,
                    Tag = "Кольца",
                    PreviewImagePath = "/upload/10.jpg",
                    ImagePaths = "/upload/10.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product()
                {
                    Id = 11,
                    CategoryId = 1,
                    Code = "K0008",
                    Name = "Кольцо \"Pond bottom\"",
                    Description = "Размер 20. Дерево ели, ювелирная смола, акрил, пигмент окраски с блёстками. Финиш морилкой, натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 1200,
                    Tag = "Кольца",
                    PreviewImagePath = "/upload/11.jpg",
                    ImagePaths = "/upload/11.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Product()
                {
                    Id = 12,
                    CategoryId = 1,
                    Code = "K0009",
                    Name = "Кольцо \"Light sections\"",
                    Description = "Размер 17. Дерево пальмы, ювелирная смола. Финиш натуральной мастикой из льняного масла и пчелиного воска.",
                    Price = 800,
                    Tag = "Кольца",
                    PreviewImagePath = "/upload/12.jpg",
                    ImagePaths = "/upload/12.jpg",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
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
