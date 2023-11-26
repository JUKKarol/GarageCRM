using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Motocomplex.DTOs.BrandModelDTOs;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.Services.BrandService;
using Motocomplex.Services.ModelService;
using Sieve.Models;

namespace Motocomplex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController(
        IModelService _modelService,
        IBrandService _brandService,
        IValidator<ModelCreateDto> _modelCreateValidator,
        IValidator<BrandModelCreateDto> _brandModelCreateValidator,
        IValidator<ModelUpdateDto> _modelUpdateValidator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetModels([FromQuery] SieveModel query)
        {
            return Ok(await _modelService.GetModels(query));
        }

        [HttpGet("{modelId}")]
        public async Task<IActionResult> GetModel(Guid modelId)
        {
            var model = await _modelService.GetModelById(modelId);

            if (model == null)
            {
                return NotFound("Model not found");
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel([FromBody] ModelCreateDto modelDto)
        {
            var validationResult = await _modelCreateValidator.ValidateAsync(modelDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            if (await _modelService.GetModelByName(modelDto.Name) != null)
            {
                return BadRequest("Model name is in use already");
            }

            if (await _brandService.GetBrandById(modelDto.brandId) == null)
            {
                return NotFound("Brand not found");
            }

            return Ok(await _modelService.CreateModel(modelDto));
        }

        [HttpPost("seed")]
        public async Task<IActionResult> CreateMassModel([FromBody] List<BrandModelCreateDto> brandModelDto)
        {
            foreach (var brandModel in brandModelDto)
            {
                var validationResult = await _brandModelCreateValidator.ValidateAsync(brandModel);
                if (!validationResult.IsValid)
                {
                    var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                    return BadRequest(string.Join(Environment.NewLine, validationErrors));
                }
            }

            foreach (var brandModel in brandModelDto)
            {
                if (await _brandService.GetBrandByName(brandModel.BrandName) != null)
                {
                    return BadRequest($"Can not duplicate brand: {brandModel.BrandName}");
                };
            }

            return Ok(await _modelService.CreateMassModel(brandModelDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateModel([FromBody] ModelUpdateDto modelDto)
        {
            var validationResult = await _modelUpdateValidator.ValidateAsync(modelDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            if (await _modelService.GetModelById(modelDto.Id) == null)
            {
                return NotFound("Model not found");
            }

            if (await _modelService.GetModelByName(modelDto.Name) != null)
            {
                return BadRequest("Model name is in use already");
            }

            if (await _brandService.GetBrandById(modelDto.brandId) == null)
            {
                return NotFound("Brand not found");
            }

            return Ok(await _modelService.UpdateModel(modelDto));
        }
    }
}