using Motocomplex.DTOs.CustomerDtos;

namespace Motocomplex.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<CustomerDetailsDto> GetCustomerById(Guid CustomerId);
        Task<CustomerDetailsDto> CreateCustomer(CustomerCreateDto customerDto);
        Task<CustomerDetailsDto> UpdateCustomer(CustomerUpdateDto customerDto);
        Task<CustomerDetailsDto> DeleteCustomer(Guid customerId);
    }
}
