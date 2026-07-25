using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace skinet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
