using Microsoft.EntityFrameworkCore;
using Pri.Drinks.Core.Entities;

namespace Pri.Drinks.Infrastructure.Data.Seeding
{
    public static class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var categories = new Category[]
            {
                new Category {Id = 1,Name = "Beer", Created = DateTime.UtcNow},
                new Category { Id = 2, Name = "Spirits", Created = DateTime.UtcNow },
            };
            var properties = new Property[]
            {
                new Property { Id = 1,Name = "Sweet", Created = DateTime.UtcNow},
                new Property { Id = 2,Name = "bitter",Created = DateTime.UtcNow},
            };
            var drinks = new Drink[]
            {
                new Drink{Id=1,Name="Duvel",AlcoholPercentage=8,CategoryId=1, Created = DateTime.UtcNow },
                new Drink{Id=2,Name="Tequila",AlcoholPercentage=38,CategoryId=2, Created = DateTime.UtcNow },
                new Drink{Id=3,Name="Irish",AlcoholPercentage=35,CategoryId=2, Created = DateTime.UtcNow },
            };
            var drinkProperties = new[]
            {
                new {DrinksId = 1,PropertiesId=1 },
                new {DrinksId = 1,PropertiesId=2 },
                new {DrinksId = 2,PropertiesId=1 },
                new {DrinksId = 2,PropertiesId=2 },
                new {DrinksId = 3,PropertiesId=2 },
                new {DrinksId = 3,PropertiesId=1 },
            };

            modelBuilder.Entity<Category>().HasData(categories);  
            modelBuilder.Entity<Property>().HasData(properties);  
            modelBuilder.Entity<Drink>().HasData(drinks);  
            modelBuilder.Entity($"{nameof(Drink)}{nameof(Property)}").HasData(drinkProperties);  
        }
    }
}
