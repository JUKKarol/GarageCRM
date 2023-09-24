using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motocomplex.DTOs.CustomerDtos;
using Motocomplex.Services.CustomerService;
using Motocomplex.Utilities.Validation;
using Motocomplex.Utilities.Validators;
using System.Security.Claims;

namespace Motocomplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<CustomerCreateDto> _customerCreateValidator;
        private readonly IValidator<CustomerUpdateDto> _customerUpdateValidator;

        public CustomerController(ICustomerService customerService, IValidator<CustomerCreateDto> customerCreateValidator, IValidator<CustomerUpdateDto> customerUpdateValidator)
        {
            _customerService = customerService;
            _customerCreateValidator = customerCreateValidator;
            _customerUpdateValidator = customerUpdateValidator;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCustomers()
        //{

        //}

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer(Guid customerId)
        {
            var customer = await _customerService.GetCustomerById(customerId);

            if (customer == null)
            { 
                return NotFound("Customer not found");
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody]CustomerCreateDto customerDto)
        {
            var validationResult = await _customerCreateValidator.ValidateAsync(customerDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            return Ok(await _customerService.CreateCustomer(customerDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateDto customerDto)
        {
            var validationResult = await _customerUpdateValidator.ValidateAsync(customerDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            if (await _customerService.GetCustomerById(customerDto.Id) == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(await _customerService.UpdateCustomer(customerDto));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid customerId)
        {
            if (await _customerService.GetCustomerById(customerId) == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(await _customerService.DeleteCustomer(customerId));
        }
    }
}
