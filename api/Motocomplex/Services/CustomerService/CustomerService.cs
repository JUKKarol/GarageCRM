﻿using AutoMapper;
using Motocomplex.Data.Repositories.CustomerRepository;
using Motocomplex.DTOs.CustomerDtos;
using Motocomplex.DTOs.SharedDTOs;
using Motocomplex.Entities;
using Sieve.Models;

namespace Motocomplex.Services.CustomerService
{
    public class CustomerService(
        ICustomerRepository _customerRepository,
        IMapper _mapper) : ICustomerService
    {
        public async Task<CustomerDetailsDto> GetCustomerById(Guid customerId)
        {
            var customer = await _customerRepository.GetCustomerById(customerId);
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
            var customer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.UpdateCustomer(customer);

            return _mapper.Map<CustomerDetailsDto>(customer);
        }

        public async Task<bool> CheckIsCustomerInArchive(Guid customerId)
        {
            var customer = await _customerRepository.GetCustomerById(customerId);

            if (customer.IsArchive == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task ChangeCustomerArchiveBool(Guid customerId, bool isArchive)
        {
            if (isArchive == true)
            {
                await _customerRepository.AddToArchive(customerId);
            }
            else
            {
                await _customerRepository.BackFromArchive(customerId);
            }
        }
    }
}