using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;

namespace Motocomplex.Data
{
    public class MotocomplexContext : IdentityDbContext
    {
        private readonly IConfiguration _configuration;

        public MotocomplexContext(DbContextOptions<MotocomplexContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repair>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Price).IsRequired();

                entity.HasOne(r => r.Car)
                    .WithMany(c => c.Repairs)
                    .HasForeignKey(r => r.CarId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(r => r.Customer)
                    .WithMany(c => c.Repairs)
                    .HasForeignKey(r => r.CustomerId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(r => r.Employees)
                   .WithMany(e => e.Repairs);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.PhoneNumber).IsRequired();

                entity.HasMany(c => c.Repairs)
                    .WithOne(r => r.Customer)
                    .HasForeignKey(r => r.CustomerId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Surname).IsRequired();

                entity.HasMany(e => e.Repairs)
                    .WithMany(r => r.Employees);
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Engine).IsRequired();
                entity.Property(c => c.Vin).IsRequired();
                entity.Property(c => c.yearOfProduction).IsRequired();
                entity.Property(c => c.ModelId).IsRequired();

                entity.HasOne(c => c.Model)
                    .WithMany(m => m.Cars)
                    .HasForeignKey(c => c.ModelId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(c => c.Repairs)
                   .WithOne(r => r.Car)
                   .HasForeignKey(r => r.CarId)
                   .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Name).IsRequired();

                entity.HasMany(b => b.Models)
                    .WithOne(m => m.Brand)
                    .HasForeignKey(m => m.brandId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Name).IsRequired();

                entity.HasOne(m => m.Brand)
                    .WithMany(b => b.Models)
                    .HasForeignKey(m => m.brandId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(m => m.Cars)
                    .WithOne(c => c.Model)
                    .HasForeignKey(c => c.ModelId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer(_configuration.GetConnectionString("MotocomplexConnectionString"));
        }
    }
}