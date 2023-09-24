using Motocomplex.DTOs.CustomerDtos;
using Motocomplex.DTOs.SharedDTOs;
using Sieve.Models;

namespace Motocomplex.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<CustomerDetailsDto> GetCustomerById(Guid CustomerId);
        Task<RespondListDto<CustomerDetailsDto>> GetCustomers(SieveModel query);
        Task<CustomerDetailsDto> CreateCustomer(CustomerCreateDto customerDto);
        Task<CustomerDetailsDto> UpdateCustomer(CustomerUpdateDto customerDto);
        Task<bool> CheckIsCustomerInArchive(Guid customerId);
        Task ChangeArchiveBool(Guid customerId, bool isArchive);
    }
}
