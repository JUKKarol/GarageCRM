using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Motocomplex.Data;
using Motocomplex.Data.Repositories.BrandRepository;
using Motocomplex.Data.Repositories.CarRepository;
using Motocomplex.Data.Repositories.CustomerRepository;
using Motocomplex.Data.Repositories.EmployeeRepository;
using Motocomplex.Data.Repositories.ModelRepository;
using Motocomplex.Data.Repositories.RepairRepository;
using Motocomplex.DTOs.BrandDTOs;
using Motocomplex.DTOs.BrandModelDTOs;
using Motocomplex.DTOs.CarDTOs;
using Motocomplex.DTOs.CustomerDtos;
using Motocomplex.DTOs.EmployeeDTOs;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.DTOs.RepairDTOs;
using Motocomplex.Middlewares;
using Motocomplex.Services.BrandService;
using Motocomplex.Services.CarService;
using Motocomplex.Services.CustomerService;
using Motocomplex.Services.EmployeeServices;
using Motocomplex.Services.ModelService;
using Motocomplex.Services.RepairService;
using Motocomplex.Utilities.Sieve;
using Motocomplex.Utilities.Validators.BrandModelValidators;
using Motocomplex.Utilities.Validators.BrandValidators;
using Motocomplex.Utilities.Validators.CarValidators;
using Motocomplex.Utilities.Validators.CustomerValidators;
using Motocomplex.Utilities.Validators.EmployeeValidators;
using Motocomplex.Utilities.Validators.ModelValidators;
using Motocomplex.Utilities.Validators.RepairValidators;
using Serilog;
using Sieve.Models;
using Sieve.Services;
using Swashbuckle.AspNetCore.Filters;

namespace Motocomplex
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.Configure<SieveOptions>(builder.Configuration.GetSection("Sieve"));

            builder.Services.AddDbContext<MotocomplexContext>();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<MotocomplexContext>();

            builder.Services.AddScoped<IValidator<CustomerCreateDto>, CustomerCreateValidator>();
            builder.Services.AddScoped<IValidator<CustomerUpdateDto>, CustomerUpdateValidator>();
            builder.Services.AddScoped<IValidator<BrandCreateDto>, BrandCreateValidator>();
            builder.Services.AddScoped<IValidator<BrandUpdateDto>, BrandUpdateValidators>();
            builder.Services.AddScoped<IValidator<ModelCreateDto>, ModelCreateValidator>();
            builder.Services.AddScoped<IValidator<ModelUpdateDto>, ModelUpdateValidator>();
            builder.Services.AddScoped<IValidator<CarCreateDto>, CarCreateValidator>();
            builder.Services.AddScoped<IValidator<CarUpdateDto>, CarUpdateValidator>();
            builder.Services.AddScoped<IValidator<EmployeeCreateDto>, EmployeeCreateValidator>();
            builder.Services.AddScoped<IValidator<EmployeeUpdateDto>, EmployeeUpdateValidator>();
            builder.Services.AddScoped<IValidator<RepairCreateDto>, RepairCreateValidator>();
            builder.Services.AddScoped<IValidator<RepairUpdateDto>, RepairUpdateValidator>();
            builder.Services.AddScoped<IValidator<BrandModelCreateDto>, BrandModelCreateValidator>();

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IModelRepository, ModelRepository>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IRepairRepository, RepairRepository>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<IModelService, ModelService>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IRepairService, RepairService>();

            builder.Services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();

            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<MotocomplexContext>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.MapIdentityApi<IdentityUser>();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}