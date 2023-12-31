﻿using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;
using System.Reflection.Emit;
using System.Text;

namespace Motocomplex.Data.Seeders
{
    public class Seeder(MotocomplexContext _db)
    {
        public void Seed(int recordsToSeed)
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
                    .RuleFor(c => c.RegistrationNumber, f => f.Random.String2(2, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + f.Random.String2(2, "0123456789") + f.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"))
                    .RuleFor(c => c.Vin, f => f.Vehicle.Vin())
                    .RuleFor(c => c.yearOfProduction, f => GenerateRandomInRange(1950, DateTime.UtcNow.Year));

                var repairGenerator = new Faker<Repair>()
                   .RuleFor(r => r.Price, f => GenerateRandomInRange(10000, 1000000))
                   .RuleFor(r => r.Description, f => f.Lorem.Sentence(GenerateRandomInRange(5, 20)));

                List<Brand> brands = new List<Brand>();
                List<Model> models = new List<Model>();
                List<Employee> employees = new List<Employee>();
                List<Customer> customers = new List<Customer>();
                List<Car> cars = new List<Car>();
                List<Repair> repairs = new List<Repair>();

                for (int i = 0; i < recordsToSeed; i++)
                {
                    var brand = brandGenerator.Generate();
                    var model = modelGenerator.Generate();
                    var employee = employeeGenerator.Generate();
                    var customer = customerGenerator.Generate();
                    var car = carGenerator.Generate();
                    var repair = repairGenerator.Generate();

                    var modelList = new List<Model> { model };
                    var employeesList = new List<Employee> { employee };
                    var repairsList = new List<Repair> { repair };

                    model.brandId = brand.Id;
                    model.Brand = brand;
                    brand.Models = modelList;
                    car.ModelId = model.Id;
                    car.Model = model;
                    repair.CarId = car.Id;
                    repair.Car = car;
                    repair.CustomerId = customer.Id;
                    repair.Customer = customer;
                    repair.Employees = employeesList;
                    employee.Repairs = repairsList;

                    brands.Add(brand);
                    models.Add(model);
                    employees.Add(employee);
                    customers.Add(customer);
                    cars.Add(car);
                    repairs.Add(repair);
                }

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
