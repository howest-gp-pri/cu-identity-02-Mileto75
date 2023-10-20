using Microsoft.EntityFrameworkCore;
using Pri.Drinks.Core.Interfaces.Repositories;
using Pri.Drinks.Core.Interfaces.Services;
using Pri.Drinks.Core.Services;
using Pri.Drinks.Infrastructure.Data;
using Pri.Drinks.Infrastructure.Repositories;

namespace Pri.Drinks.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Db service
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options
                .UseSqlServer(builder.Configuration
                .GetConnectionString("ApplicationDb")));
            //register the repository service
            builder.Services.AddTransient<IDrinkRepository, DrinkRepository>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.Services.AddTransient<IPropertyRepository, PropertyRepository>();
            builder.Services.AddTransient<IFileService, FileService>();
            builder.Services.AddTransient<IDrinkService, DrinkService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}