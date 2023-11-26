using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Motocomplex.DTOs.BrandDTOs;
using Motocomplex.Services.BrandService;
using Sieve.Models;

namespace Motocomplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(
        IBrandService _brandService,
        IValidator<BrandCreateDto> _brandCreateValidator,
        IValidator<BrandUpdateDto> _brandUpdateValidator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBrands([FromQuery] SieveModel query)
        {
            return Ok(await _brandService.GetBrands(query));
        }

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetBrand(Guid brandId)
        {
            var brand = await _brandService.GetBrandById(brandId);

            if (brand == null)
            {
                return NotFound("Brand not found");
            }

            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] BrandCreateDto brandDto)
        {
            var validationResult = await _brandCreateValidator.ValidateAsync(brandDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            if (await _brandService.GetBrandByName(brandDto.Name) != null)
            {
                return BadRequest("Brand name is in use already");
            }

            return Ok(await _brandService.CreateBrand(brandDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand([FromBody] BrandUpdateDto brandDto)
        {
            var validationResult = await _brandUpdateValidator.ValidateAsync(brandDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            if (await _brandService.GetBrandById(brandDto.Id) == null)
            {
                return NotFound("Brand not found");
            }

            if (await _brandService.GetBrandByName(brandDto.Name) != null)
            {
                return BadRequest("Brand name is in use already");
            }

            return Ok(await _brandService.UpdateBrand(brandDto));
        }
    }
}