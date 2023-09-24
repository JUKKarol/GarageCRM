
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Motocomplex.Data;
using Motocomplex.Data.Repositories.CustomerRepository;
using Motocomplex.DTOs.CustomerDtos;
using Motocomplex.Services.CustomerService;
using Motocomplex.Utilities.Validation;
using Motocomplex.Utilities.Validators;

namespace Motocomplex
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MotocomplexContext>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<IValidator<CustomerCreateDto>, CustomerCreateValidator>();
            builder.Services.AddScoped<IValidator<CustomerUpdateDto>, CustomerUpdateValidator>();

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();

            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<MotocomplexContext>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}