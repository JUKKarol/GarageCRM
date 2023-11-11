using Motocomplex.DTOs.BrandModelDTOs;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Sieve.Models;

namespace Motocomplex.Services.ModelService
{
    public interface IModelService
    {
        Task<ModelDetalisDto> GetModelById(Guid modelId);

        Task<ModelDetalisDto> GetModelByName(string modelName);

        Task<RespondListDto<ModelDetalisDto>> GetModels(SieveModel query);

        Task<ModelDetalisDto> CreateModel(ModelCreateDto modelDto);

        Task<List<ModelDetalisDto>> CreateMassModel(List<BrandModelCreateDto> brandModelDto);

        Task<ModelDetalisDto> UpdateModel(ModelUpdateDto modelDto);
    }
}