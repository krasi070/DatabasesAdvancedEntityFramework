namespace _01.ProductShop
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using DTOs;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        // Problem 02 - Import data (in ProductShopInitializer)
        public static void Main()
        {
            InitMappers();

            PrintUsersWithSoldProducts();
        }

        private static void InitMappers()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>()
                    .ForMember(dto => dto.Seller,
                    opt => opt.MapFrom(src => (src.Seller.FirstName + " " + src.Seller.LastName).Trim()));
                cfg.CreateMap<Product, SoldProductDto>();
                cfg.CreateMap<User, SellerDto>();
                cfg.CreateMap<Category, CategoryDto>()
                    .ForMember(dto => dto.Category,
                    opt => opt.MapFrom(src => src.Name))
                    .ForMember(dto => dto.ProductsCount,
                    opt => opt.MapFrom(src => src.Products.Count))
                    .ForMember(dto => dto.AveragePrice,
                    opt => opt.MapFrom(src => src.Products.Average(p => p.Price)))
                    .ForMember(dto => dto.TotalRevenue,
                    opt => opt.MapFrom(src => src.Products.Sum(p => p.Price)));
            });
        }

        // Problem 03 - Query and Export Data
        // Query 1 - Products In Range
        private static void PrintProductsInRange()
        {
            using (var context = new ProductShopContext())
            {
                var products = context.Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .OrderBy(p => p.Price)
                    .ProjectTo<ProductDto>()
                    .ToList();

                Console.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));
            }
        }

        // Query 2 - Successfully Sold Products
        private static void PrintSellers()
        {
            using (var context = new ProductShopContext())
            {
                var sellers = context.Users
                    .Where(s => s.ProductsSold.Any(p => p.Buyer != null))
                    .OrderBy(s => s.FirstName)
                    .ThenBy(s => s.LastName);

                var sellerDtos = sellers
                    .ProjectTo<SellerDto>()
                    .ToList();

                int index = 0;
                foreach (var seller in sellers)
                {
                    var products = seller.ProductsSold
                        .Where(p => p.Buyer != null)
                        .ToList();
                    var productDtos = Mapper.Map<List<Product>, List<SoldProductDto>>(products);
                    sellerDtos[index].SoldProducts = productDtos;

                    index++;
                }
                     
                Console.WriteLine(JsonConvert.SerializeObject(sellerDtos, Formatting.Indented));
            }
        }

        // Query 3 - Categories By Products Count
        private static void PrintCategories()
        {
            using (var context = new ProductShopContext())
            {
                var categories = context.Categories
                    .OrderBy(c => c.Name)
                    .ProjectTo<CategoryDto>()
                    .ToList();

                Console.WriteLine(JsonConvert.SerializeObject(categories, Formatting.Indented));
            }
        }

        // Query 4 - Users and Products
        private static void PrintUsersWithSoldProducts()
        {
            using (var context = new ProductShopContext())
            {
                var usersWithSoldProducts = new
                {
                    UsersCount = context.Users
                        .Where(u => u.ProductsSold.Count > 0)
                        .Count(),
                    Users = context.Users
                        .Where(u => u.ProductsSold.Count > 0)
                        .Select(u => new
                        {
                            u.FirstName,
                            u.LastName,
                            u.Age,
                            SoldProducts = new
                            {
                                Count = u.ProductsSold.Count,
                                Products = u.ProductsSold
                                    .Select(p => new
                                    {
                                        p.Name,
                                        p.Price
                                    })
                            }
                        })
                        .OrderByDescending(u => u.SoldProducts.Count)
                        .ThenBy(u => u.LastName)
                };

                Console.WriteLine(JsonConvert.SerializeObject(usersWithSoldProducts, Formatting.Indented));
            }
        }
    }
}