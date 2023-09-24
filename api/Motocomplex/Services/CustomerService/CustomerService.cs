using AutoMapper;
using Motocomplex.Data.Repositories.CustomerRepository;
using Motocomplex.DTOs.CustomerDtos;
using Motocomplex.Entities;
using Motocomplex.Utilities.Mappings;

namespace Motocomplex.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDetailsDto> GetCustomerById(Guid CustomerId)
        {
            var customer = await _customerRepository.GetCustomerById(CustomerId);
            return _mapper.Map<CustomerDetailsDto>(customer);
        }

        //get customer list with search

        public async Task<CustomerDetailsDto> CreateCustomer(CustomerCreateDto customerDto)
        {
            var cusotmer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.CreateCustomer(cusotmer);

            return _mapper.Map<CustomerDetailsDto>(cusotmer);
        }

        public async Task<CustomerDetailsDto> UpdateCustomer(CustomerUpdateDto customerDto)
        {
            var cusotmer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.UpdateCustomer(cusotmer);

            return _mapper.Map<CustomerDetailsDto>(cusotmer);
        }
        public async Task<CustomerDetailsDto> DeleteCustomer(Guid customerId)
        {
            var customer = await _customerRepository.DeleteCustomer(customerId);

            return _mapper.Map<CustomerDetailsDto>(customer);
        }
    }
}
