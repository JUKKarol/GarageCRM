using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;
using System.Reflection.Emit;
using System.Text;

namespace Motocomplex.Data.Seeders
{
    public class Seeder
    {
        private readonly MotocomplexContext _db;

        public Seeder(MotocomplexContext db)
        {
            _db = db;
        }
        public void Seed()
        {
            if (!_db.Brands.Any() && !_db.Cars.Any() && !_db.Customers.Any() && !_db.Employee.Any() && !_db.Repairs.Any())
            {
                var locale = "pl";

                var brandGenerator = new Faker<Brand>()
                    .RuleFor(b => b.Name, f => f.Vehicle.Manufacturer());

                var modelGenerator = new Faker<Model>()
                    .RuleFor(m => m.Name, f => f.Vehicle.Model());

                var employeeGenerator = new Faker<Employee>(locale)
                    .RuleFor(e => e.Name, f => f.Person.FirstName)
                    .RuleFor(e => e.Surname, f => f.Person.LastName);

                var customerGenerator = new Faker<Customer>(locale)
                    .RuleFor(c => c.Name, f => f.Person.FullName)
                    .RuleFor(c => c.PhoneNumber, f => f.Person.Phone)
                    .RuleFor(c => c.Email, f => f.Person.Email)
                    .RuleFor(c => c.Nip, f => GenerateNIP())
                    .RuleFor(c => c.City, f => f.Person.Address.City)
                    .RuleFor(c => c.Address, f => f.Person.Address.Street)
                    .RuleFor(c => c.PostalCode, f => f.Person.Address.ZipCode);

                var carGenerator = new Faker<Car>()
                    .RuleFor(c => c.Engine, f => GenerateRandomInRange(800, 4000))
                    .RuleFor(c => c.RegistrationNumber, f => f.Vehicle.Vin())
                    .RuleFor(c => c.Vin, f => f.Random.String2(2, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + f.Random.String2(2, "0123456789") + f.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"))
                    .RuleFor(c => c.yearOfProduction, f => GenerateRandomInRange(1950, DateTime.UtcNow.Year));

                var repairGenerator = new Faker<Repair>()
                   .RuleFor(r => r.Price, f => GenerateRandomInRange(10000, 1000000))
                   .RuleFor(r => r.Description, f => f.Lorem.Sentence(GenerateRandomInRange(5, 20)));

                List<Brand> brands;
                List<Brand> models;
                List<Brand> employees;
                List<Brand> customers;
                List<Brand> cars;
                List<Brand> repairs;

                for (int i = 0; i < 10; i++)
                {
                    var brand = brandGenerator.Generate();

                    var model = modelGenerator.Generate();
                    model.brandId = brand.Id;

                    var employee = employeeGenerator.Generate();
                    var customer = customerGenerator.Generate();

                    var car = carGenerator.Generate();
                    car.ModelId = model.Id;

                    var repair = repairGenerator.Generate();
                    var employeesList = new List<Employee> { employee };
                    var repairsList = new List<Repair> { repair };
                    repair.CarId = car.Id;
                    repair.CustomerId = customer.Id;
                    repair.Employees = employeesList;
                    employee.Repairs = repairsList;
                }

                //var brands = brandGenerator.Generate(100);
                //var models = modelGenerator.Generate(100);
                //var employees = employeeGenerator.Generate(100);
                //var customers = customerGenerator.Generate(100);
                //var cars = carGenerator.Generate(100);
                //var repairs = repairGenerator.Generate(100);

                _db.AddRange(brands);
                _db.AddRange(models);
                _db.AddRange(employees);
                _db.AddRange(customers);
                _db.AddRange(cars);
                _db.AddRange(repairs);

                _db.SaveChanges();
            }

            string GenerateNIP()
            {
                var random = new Random();
                var builder = new StringBuilder();

                for (int i = 0; i < 10; i++)
                {
                    builder.Append(random.Next(10));
                }

                return builder.ToString();
            }

            int GenerateRandomInRange(int min, int max)
            {
                Random random = new Random();
                return random.Next(min, max); 
            }
        }
    }
}
