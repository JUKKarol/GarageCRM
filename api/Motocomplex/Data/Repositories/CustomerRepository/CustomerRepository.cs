using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;
using Sieve.Models;
using Sieve.Services;

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

        public async Task<Customer> GetCustomerById(Guid customerId)
        {
            return await _db.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
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
            var customerIsArchive = customer.IsArchive;
            updatedCustomer.CreatedAt = customerCraetedAt;
            updatedCustomer.IsArchive = customerIsArchive;

            var entry = _db.Entry(customer);
            entry.CurrentValues.SetValues(updatedCustomer);

            await _db.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> AddToArchive(Guid customerId)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            customer.IsArchive = true;
            await _db.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> BackFromArchive(Guid customerId)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            customer.IsArchive = false;
            await _db.SaveChangesAsync();

            return customer;
        }
    }
}