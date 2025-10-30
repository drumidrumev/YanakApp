using Microsoft.EntityFrameworkCore;
using YanakApp.Contracts;
using YanakApp.Models;
using YanakApp.Services;

namespace YanakApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("ShopDbConnectionString");
            builder.Services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IProductsService, ProductsService>();

            builder.Services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
