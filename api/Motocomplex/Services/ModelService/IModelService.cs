using Motocomplex.DTOs.BrandModelDTOs;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Sieve.Models;

namespace Motocomplex.Services.ModelService
{
    public interface IModelService
    {
        Task<ModelDetailsDto> GetModelById(Guid modelId);

        Task<ModelDetailsDto> GetModelByName(string modelName);

        Task<RespondListDto<ModelDetailsDto>> GetModels(SieveModel query);

        Task<ModelDetailsDto> CreateModel(ModelCreateDto modelDto);

        Task<List<ModelDetailsDto>> CreateMassModel(List<BrandModelCreateDto> brandModelDto);

        Task<ModelDetailsDto> UpdateModel(ModelUpdateDto modelDto);
    }
}