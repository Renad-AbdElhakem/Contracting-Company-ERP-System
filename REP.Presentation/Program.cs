
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace REP.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ApplicationDbContext>(option=>
            option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));
            
            var app = builder.Build();


            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();

            await ContextDataSeed.SeedAsync(dbContext);



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
