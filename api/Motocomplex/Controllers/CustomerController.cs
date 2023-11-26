using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Motocomplex.DTOs.CustomerDtos;
using Motocomplex.Services.CustomerService;
using Sieve.Models;

namespace Motocomplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(
        ICustomerService _customerService,
        IValidator<CustomerCreateDto> _customerCreateValidator,
        IValidator<CustomerUpdateDto> _customerUpdateValidator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] SieveModel query)
        {
            return Ok(await _customerService.GetCustomers(query));
        }

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
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto customerDto)
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

        [HttpPut("archive")]
        public async Task<IActionResult> ArchiveCustomer([FromQuery] Guid customerId)
        {
            if (await _customerService.GetCustomerById(customerId) == null)
            {
                return NotFound("Customer not found");
            }

            if (await _customerService.CheckIsCustomerInArchive(customerId) == true)
            {
                return BadRequest("Customer is in archive already");
            }

            await _customerService.ChangeCustomerArchiveBool(customerId, true);

            return Ok("Customer archived");
        }

        [HttpPut("archive/back")]
        public async Task<IActionResult> ArchiveBackCustomer([FromQuery] Guid customerId)
        {
            if (await _customerService.GetCustomerById(customerId) == null)
            {
                return NotFound("Customer not found");
            }

            if (await _customerService.CheckIsCustomerInArchive(customerId) == false)
            {
                return BadRequest("Customer is not in archive");
            }

            await _customerService.ChangeCustomerArchiveBool(customerId, false);

            return Ok("Customer archived back");
        }
    }
}