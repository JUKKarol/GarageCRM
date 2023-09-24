using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;

namespace Motocomplex.Data.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MotocomplexContext _db;

        public CustomerRepository(MotocomplexContext db)
        {
            _db = db;
        }

        public async Task<Customer> GetCustomerById(Guid CustomerId)
        {
            return await _db.Customers.FirstOrDefaultAsync(c => c.Id == CustomerId);
        }

        //get customers list with search

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
