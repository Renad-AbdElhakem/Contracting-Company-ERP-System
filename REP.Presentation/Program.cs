
using ERP.Application.AutoMapper;
using ERP.Application.AutoMapper.Module_1_Project_Site_Management;
using ERP.Application.AutoMapper.Module_2__Procurement_Inventory;
using ERP.Application.AutoMapper.Module_3___Equipment_Machinery;
using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Repository.Module_3___Equipment_Machinery;
using ERP.Application.Interfaces.Services;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_3___Equipment_Machinery;
using ERP.Infrastructure.Persistence;
using ERP.Infrastructure.Repository.Module_1_Project_Site_Management;
using ERP.Infrastructure.Repository.Module_2__Procurement_Inventory;
using ERP.Infrastructure.Repository.Module_3___Equipment_Machinery;
using ERP.Infrastructure.Services.Module_1_Project_Site_Management;
using ERP.Infrastructure.Services.Module_2__Procurement_Inventory.SupplierHandler;
using ERP.Infrastructure.Services.Module_3___Equipment_Machinery;
using MediatR;
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
                cfg.AddProfile<EquipmentProfile>();
                cfg.AddProfile<ProjectProfile>();
                cfg.AddProfile<SupplierProfile>();
                cfg.AddProfile<MaterialProfile>();
                cfg.AddProfile<CompanyWarehouseProfile>();
                cfg.AddProfile<ProjectWarehouseProfile>();
                cfg.AddProfile<MaterialRequirementProfile>();
                cfg.AddProfile<MaterialConsumptionProfile>();

            });

            builder.Services.AddMediatR(cfg =>cfg.RegisterServicesFromAssembly(typeof(CreateNewSupplierCommandHandler).Assembly));
            
            
            #region Repository

            #region Module_1_Project_Site_Management(Repository)

            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectEmployeeRepository, ProjectEmployeeRepository>();
            builder.Services.AddScoped<IProjectPhaseRepository, ProjectPhaseRepository>();
            builder.Services.AddScoped<IPhaseRepository, PhaseRepository>();

            #endregion




            #region Module 3 — Equipment&Machinery(Repository)

            builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            builder.Services.AddScoped<IMaintenanceEmployeeRepository, MaintenanceEmployeeRepository>();
            builder.Services.AddScoped<IProjectEquipmentRepository, ProjectEquipmentRepository>();

            #endregion

            #region Module_2 _Procurement&Inventory(Repository)

            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
            builder.Services.AddScoped<ISupplierMaterialPriceRepository, SupplierMaterialPriceRepository>();
            builder.Services.AddScoped<ICompanyWarehouseRepository, CompanyWarehouseRepository>();
            builder.Services.AddScoped<IProjectWarehouseRepository, ProjectWarehouseRepository>();
            builder.Services.AddScoped<IPhaseMaterialRequirementRepository, PhaseMaterialRequirementRepository>();
            builder.Services.AddScoped<IProjectWarehouseStockRepository, ProjectWarehouseStockRepository>();
       
            #endregion

            #endregion



            #region Module_1_Project_Site_Management(Service)

            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IProjectPhasesService, ProjectPhasesService>();
            builder.Services.AddScoped<IPhaseService, PhaseService>();

            #endregion

            #region Module 3 — Equipment&Machinery(Service)
            builder.Services.AddScoped<IEquipmentService, EquipmentService>();
            builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
            builder.Services.AddScoped<IProjectEquipmentService, ProjectEquipmentService>();

            #endregion

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
