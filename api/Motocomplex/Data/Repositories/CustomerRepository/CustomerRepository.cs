using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;
using Sieve.Models;
using Sieve.Services;
using System;

namespace Motocomplex.Data.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MotocomplexContext _db;
        private readonly ISieveProcessor _sieveProcessor;

        public CustomerRepository(MotocomplexContext db, ISieveProcessor sieveProcessor)
        {
            _db = db;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Customer> GetCustomerById(Guid CustomerId)
        {
            return await _db.Customers.FirstOrDefaultAsync(c => c.Id == CustomerId);
        }

        public async Task<List<Customer>> GetCustomers(SieveModel query)
        {
            var customers = _db
                .Customers
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, customers)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetCustomersCount(SieveModel query)
        {
            var customers = _db
                .Customers
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, customers, applyPagination: false)
                .CountAsync();
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateCustomer(Customer updatedCustomer)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == updatedCustomer.Id);

            var customerCraetedAt = customer.CreatedAt;
            customer = updatedCustomer;
            customer.CreatedAt = customerCraetedAt;

            await _db.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> DeleteCustomer(Guid customerId)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();

            return customer;
        }
    }
}
