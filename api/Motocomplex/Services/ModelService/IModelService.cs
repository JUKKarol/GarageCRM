using Motocomplex.DTOs.BrandModelDTOs;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Sieve.Models;

namespace Motocomplex.Services.ModelService
{
    public interface IModelService
    {
        Task<ModelDisplayDto> GetModelById(Guid modelId);

        Task<ModelDisplayDto> GetModelByName(string modelName);

        Task<RespondListDto<ModelDisplayDto>> GetModels(SieveModel query);

        Task<ModelDisplayDto> CreateModel(ModelCreateDto modelDto);

        Task<List<ModelDisplayDto>> CreateMassModel(List<BrandModelCreateDto> brandModelDto);

        Task<ModelDisplayDto> UpdateModel(ModelUpdateDto modelDto);
    }
}