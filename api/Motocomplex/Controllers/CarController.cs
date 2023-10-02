using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Motocomplex.DTOs.BrandDTOs;
using Motocomplex.DTOs.CarDTOs;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.Services.BrandService;
using Motocomplex.Services.CarService;
using Motocomplex.Services.ModelService;
using Motocomplex.Utilities.Validators.ModelValidators;
using Sieve.Models;

namespace Motocomplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IModelService _modelService;
        private readonly IValidator<CarCreateDto> _carCreateValidator;
        private readonly IValidator<CarUpdateDto> _carUpdateValidator;

        public CarController(ICarService carService, IModelService modelService, IValidator<CarCreateDto> carCreateValidator, IValidator<CarUpdateDto> carUpdateValidator)
        {
            _carService = carService;
            _modelService = modelService;
            _carCreateValidator = carCreateValidator;
            _carUpdateValidator = carUpdateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars([FromQuery] SieveModel query)
        {
            return Ok(await _carService.GetCars(query));
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCar(Guid carId)
        {
            var car = await _carService.GetCarById(carId);

            if (car == null)
            {
                return NotFound("Car not found");
            }

            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CarCreateDto carDto)
        {
            var validationResult = await _carCreateValidator.ValidateAsync(carDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            if (await _carService.GetCarByVin(carDto.Vin) != null)
            {
                return BadRequest("Car vin is in use already");
            }

            if (await _carService.GetCarByRegistrationNumber(carDto.RegistrationNumber) != null)
            {
                return BadRequest("Car registration number is in use already");
            }

            if (await _modelService.GetModelById(carDto.ModelId) == null)
            {
                return NotFound("Model not found");
            }

            return Ok(await _carService.CreateCar(carDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCar([FromBody] CarUpdateDto carDto)
        {
            var validationResult = await _carUpdateValidator.ValidateAsync(carDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            var currentCar = await _carService.GetCarById(carDto.Id);

            if (currentCar == null)
            {
                return NotFound("Car not found");
            }

            if (await _carService.GetCarByVin(carDto.Vin) != null && currentCar.Vin != carDto.Vin)
            {
                return BadRequest("Car vin is in use already");
            }

            if (await _carService.GetCarByRegistrationNumber(carDto.RegistrationNumber) != null && currentCar.RegistrationNumber != carDto.RegistrationNumber)
            {
                return BadRequest("Car registration number is in use already");
            }

            if (await _modelService.GetModelById(carDto.ModelId) == null)
            {
                return NotFound("Model not found");
            }

            return Ok(await _carService.UpdateCar(carDto));
        }
    }
}
