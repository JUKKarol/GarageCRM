using AutoMapper;
using Motocomplex.Data.Repositories.CustomerRepository;
using Motocomplex.DTOs.CustomerDtos;
using Motocomplex.DTOs.SharedDTOs;
using Motocomplex.Entities;
using Motocomplex.Utilities.Mappings;
using Sieve.Models;

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

        public async Task<RespondListDto<CustomerDetailsDto>> GetCustomers(SieveModel query)
        {
            int pageSize = query.PageSize != null ? (int)query.PageSize : 40;

            var customers = await _customerRepository.GetCustomers(query);
            var customersDto = _mapper.Map<List<CustomerDetailsDto>>(customers);

            RespondListDto<CustomerDetailsDto> respondListDto = new RespondListDto<CustomerDetailsDto>();
            respondListDto.Items = customersDto;
            respondListDto.ItemsCount = await _customerRepository.GetCustomersCount(query);
            respondListDto.PagesCount = (int)Math.Ceiling((double)respondListDto.ItemsCount / pageSize);

            return respondListDto;
        }

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
