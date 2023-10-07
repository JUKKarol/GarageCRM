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

        Task<ModelDisplayDto> UpdateModel(ModelUpdateDto modelDto);
    }
}