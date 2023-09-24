using Motocomplex.Entities;
using Sieve.Models;

namespace Motocomplex.Data.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(Guid CustomerId);
        Task<List<Customer>> GetCustomers(SieveModel query);
        Task<int> GetCustomersCount(SieveModel query);
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer updatedCustomer);
        Task<Customer> DeleteCustomer(Guid customerId);
    }
}
