using ERP.Domain.Model;
using ERP.Domain.Model.Module_1_Project_Site_Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ERP.Infrastructure.Persistence
{
    public class ContextDataSeed
    {


        public static async Task SeedAsync(ApplicationDbContext dbContext)
        {

            using var transaction = dbContext.Database.BeginTransaction();

            try
            {
                //--------------------Roles-----------------------

                if (!dbContext.Roles.Any())
                {
                    var readDataFromJsonFile = File.ReadAllBytes("../ERP.Infrastructure/Persistence/DataSeed/roles.json");

                    var dataSeedRole = JsonSerializer.Deserialize<List<Role>>(readDataFromJsonFile);

                    if (dataSeedRole != null)
                    {

                        await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles ON");
                        try
                        {

                            await dbContext.Roles.AddRangeAsync(dataSeedRole);

                            await dbContext.SaveChangesAsync();
                        }
                        finally
                        {

                            await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles OFF");

                        }

                    }

                }

                //---------------------Departments---------------------

                if (!dbContext.Departments.Any())
                {
                    var readDepartmentFromjsonfile = File.ReadAllBytes("../ERP.Infrastructure/Persistence/DataSeed/departments.json");

                    var dataSeedDepartment = JsonSerializer.Deserialize<List<Department>>(readDepartmentFromjsonfile);
                    if (dataSeedDepartment != null)
                    {

                        await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Departments ON");

                        try
                        {
                            await dbContext.Departments.AddRangeAsync(dataSeedDepartment);

                            await dbContext.SaveChangesAsync();

                        }
                        finally
                        {
                            await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Departments OFF");
                        }


                    }
                }
                //-----------------------Employees------------------------
                if (!dbContext.Employees.Any())
                {
                    var readEmployeesFromjsonfile = File.ReadAllBytes("../ERP.Infrastructure/Persistence/DataSeed/employees.json");

                    var dataSeedEmployees = JsonSerializer.Deserialize<List<Employee>>(readEmployeesFromjsonfile);
                    if (dataSeedEmployees != null)
                    {

                        await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Employees ON");
                       
                        try
                        {

                            await dbContext.Employees.AddRangeAsync(dataSeedEmployees);

                            await dbContext.SaveChangesAsync();
                        }
                        finally
                        {

                            await dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Employees OFF");
                        }

                    }
                }

                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {

                await transaction.RollbackAsync();
                throw;
            }






        }
    }
}
