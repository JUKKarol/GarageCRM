using Motocomplex.Entities;

namespace Motocomplex.Data.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(Guid CustomerId);
        //get customers list with search
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer updatedCustomer);
        Task<Customer> DeleteCustomer(Guid customerId);
    }
}
