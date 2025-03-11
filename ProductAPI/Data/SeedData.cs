using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(ProductDbContext context)
        {
            if (context.Products.Any())
            {
                return; 
            }

            context.Products.AddRange(new List<Product>
            {
                new Product { Name = "Laptop", Price = 1200.99m },
                new Product { Name = "Smartphone", Price = 800.50m },
                new Product { Name = "Tablet", Price = 450.75m }
            });
            context.SaveChanges();
        }
    }
}