using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Motocomplex.DTOs.EmployeeDTOs;
using Motocomplex.Services.EmployeeServices;
using Sieve.Models;

namespace Motocomplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(
        IEmployeeService _employeeService,
        IValidator<EmployeeCreateDto> _employeeCreateValidator,
        IValidator<EmployeeUpdateDto> _employeeUpdateValidator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] SieveModel query)
        {
            return Ok(await _employeeService.GetEmployees(query));
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployee(Guid employeeId)
        {
            var employee = await _employeeService.GetEmployeeById(employeeId);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto employeeDto)
        {
            var validationResult = await _employeeCreateValidator.ValidateAsync(employeeDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            return Ok(await _employeeService.CreateEmployee(employeeDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeUpdateDto employeeDto)
        {
            var validationResult = await _employeeUpdateValidator.ValidateAsync(employeeDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            if (await _employeeService.GetEmployeeById(employeeDto.Id) == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(await _employeeService.UpdateEmployee(employeeDto));
        }

        [HttpPut("archive")]
        public async Task<IActionResult> ArchiveEmployee([FromQuery] Guid employeeId)
        {
            if (await _employeeService.GetEmployeeById(employeeId) == null)
            {
                return NotFound("Employee not found");
            }

            if (await _employeeService.CheckIsEmployeeInArchive(employeeId) == true)
            {
                return BadRequest("Employee is in archive already");
            }

            await _employeeService.ChangeEmployeeArchiveBool(employeeId, true);

            return Ok("Employee archived");
        }

        [HttpPut("archive/back")]
        public async Task<IActionResult> ArchiveBackEmployee([FromQuery] Guid employeeId)
        {
            if (await _employeeService.GetEmployeeById(employeeId) == null)
            {
                return NotFound("Employee not found");
            }

            if (await _employeeService.CheckIsEmployeeInArchive(employeeId) == false)
            {
                return BadRequest("Employee is not in archive");
            }

            await _employeeService.ChangeEmployeeArchiveBool(employeeId, false);

            return Ok("Employee archived back");
        }
    }
}