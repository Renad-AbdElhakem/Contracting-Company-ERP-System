
using ERP.Application.AutoMapper;
using ERP.Application.AutoMapper.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Infrastructure.Persistence;
using ERP.Infrastructure.Repository.Module_1_Project_Site_Management;
using ERP.Infrastructure.Services.Module_1_Project_Site_Management;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

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

            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));


            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RoleProfile>();
                cfg.AddProfile<EmployeeProfile>();
                cfg.AddProfile<ClientProfile>();
                cfg.AddProfile<DepartmentProfile>();
                cfg.AddProfile<ProjectProfile>();
                //cfg.AddProfile<MemberProfile>();
                //cfg.AddProfile<SubscriptionTypeMappingProfile>();
                //cfg.AddProfile<AttendanceMemberProfile>();
                //cfg.AddProfile<EmployeeAttendanceProfile>();
                //cfg.AddProfile<LeaveRequestProfile>();

            });

            // builder.Services.AddAutoMapper();

            //--------------------Module_1_Project_Site_Management(Repository)-----------------------

            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectEmployeeRepository, ProjectEmployeeRepository>();
            builder.Services.AddScoped<IProjectPhaseRepository, ProjectPhaseRepository>();

            //--------------------Module_1_Project_Site_Management(Service)-----------------------

            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IProjectPhasesService, ProjectPhasesService>();


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
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
