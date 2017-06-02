using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PurchasePool.Data.EF.Entities;

namespace PurchasePool.Data.EF.Initializers
{
    class CreateDatabaseInitializer: CreateDatabaseIfNotExists<MainDataContext>
    {
        protected override void Seed(MainDataContext context)
        {
            context.Categories.AddRange(new List<Category> {
                new Category { Name = "Mediun quality", Description = "It's enough good products."},
                new Category { Name = "High quality", Description = "It's very cool products." },
                new Category { Name = "Low quality", Description = "It's bad products." }
            });

            context.Goods.AddRange(new List<Good>() {
                new Good {Name = "Good product", Description = "A good product", WebLink = @"http:\\products.com\goodproduct"},
                new Good {Name = "Not good product", Description = "A not good product", WebLink = @"http:\\products.com\anotgoodproduct"},
                new Good {Name = "Bad product", Description = "A bad product", WebLink = @"http:\\products.com\abadproduct"}
            });

            context.CategoryGoodReferences.AddRange(new List<CategoryGoodReference> {
                new CategoryGoodReference{
                    Good = context.Goods.FirstOrDefault(g => g.Name == "Good product"),
                    Category = context.Categories.FirstOrDefault(g => g.Name == "High quality")
                },
                new CategoryGoodReference{
                    Good = context.Goods.FirstOrDefault(g => g.Name == "Bad product"),
                    Category = context.Categories.FirstOrDefault(g => g.Name == "Low quality")
                },
                new CategoryGoodReference{
                    Good = context.Goods.FirstOrDefault(g => g.Name == "Not good product"),
                    Category = context.Categories.FirstOrDefault(g => g.Name == "Low quality")
                },
                new CategoryGoodReference{
                    Good = context.Goods.FirstOrDefault(g => g.Name == "Not good product"),
                    Category = context.Categories.FirstOrDefault(g => g.Name == "Mediun quality")
                },
            });

            context.SaveChanges();
        }
    }
}
