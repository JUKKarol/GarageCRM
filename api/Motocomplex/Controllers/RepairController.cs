using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Motocomplex.DTOs.RepairDTOs;
using Motocomplex.Enums;
using Motocomplex.Services.CarService;
using Motocomplex.Services.CustomerService;
using Motocomplex.Services.EmployeeServices;
using Motocomplex.Services.RepairService;
using Sieve.Models;

namespace Motocomplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairController : Controller
    {
        private readonly IRepairService _repairService;
        private readonly IEmployeeService _employeeService;
        private readonly ICustomerService _customerService;
        private readonly ICarService _carService;
        private readonly IValidator<RepairCreateDto> _repairCreateValidator;
        private readonly IValidator<RepairUpdateDto> _repairUpdateValidator;

        public RepairController(IRepairService repairService, IEmployeeService employeeService, ICustomerService customerService, ICarService carService, IValidator<RepairCreateDto> repairCreateValidator, IValidator<RepairUpdateDto> repairUpdateValidator)
        {
            _repairService = repairService;
            _employeeService = employeeService;
            _customerService = customerService;
            _carService = carService;
            _repairCreateValidator = repairCreateValidator;
            _repairUpdateValidator = repairUpdateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRepairs([FromQuery] SieveModel query)
        {
            return Ok(await _repairService.GetRepairs(query));
        }

        [HttpGet("{repairId}")]
        public async Task<IActionResult> GetRepair(Guid repairId)
        {
            var repair = await _repairService.GetRepairById(repairId);

            if (repair == null)
            {
                return NotFound("Repair not found");
            }

            return Ok(repair);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRepair([FromBody] RepairCreateDto repairDto)
        {
            var validationResult = await _repairCreateValidator.ValidateAsync(repairDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            if (await _customerService.GetCustomerById(repairDto.CustomerId) == null)
            {
                return NotFound("Customer not found");
            }

            if (await _carService.GetCarById(repairDto.CarId) == null)
            {
                return NotFound("Car not found");
            }

            if (!await _employeeService.CheckIsAllEmployeesExist(repairDto.EmployeesIds))
            {
                return NotFound("One or more Employees not found");
            }

            return Ok(await _repairService.CreateRepair(repairDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRepair([FromBody] RepairUpdateDto repairDto)
        {
            var validationResult = await _repairUpdateValidator.ValidateAsync(repairDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            if (await _repairService.GetRepairById(repairDto.Id) == null)
            {
                return NotFound("Repair not found");
            }

            if (await _customerService.GetCustomerById(repairDto.CustomerId) == null)
            {
                return NotFound("Customer not found");
            }

            if (await _carService.GetCarById(repairDto.CarId) == null)
            {
                return NotFound("Car not found");
            }

            if (await _employeeService.CheckIsAllEmployeesExist(repairDto.EmployeesIds))
            {
                return NotFound("One or more Employees not found");
            }

            return Ok(await _repairService.UpdateRepair(repairDto));
        }

        [HttpPut("status")]
        public async Task<IActionResult> ChangeRepairStatus([FromQuery] Guid repairId, RepairStatus status)
        {
            var repair = await _repairService.GetRepairById(repairId);

            if (repair == null)
            {
                return NotFound("Repair not found");
            }

            if (repair.Status == status)
            {
                return BadRequest("Status you trying to change is current already");
            }

            await _repairService.ChangeRepairStatus(repairId, status);

            return Ok("Status changed");
        }
    }
}