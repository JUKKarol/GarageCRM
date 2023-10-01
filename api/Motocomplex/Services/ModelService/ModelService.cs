using AutoMapper;
using Motocomplex.Data.Repositories.ModelRepository;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Motocomplex.Entities;
using Sieve.Models;

namespace Motocomplex.Services.ModelService
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public ModelService(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<ModelDisplayDto> GetModelById(Guid modelId)
        {
            var model = await _modelRepository.GetModelById(modelId);
            return _mapper.Map<ModelDisplayDto>(model);
        }

        public async Task<ModelDisplayDto> GetModelByName(string modelName)
        {
            var model = await _modelRepository.GetModelByNAme(modelName);
            return _mapper.Map<ModelDisplayDto>(model);
        }

        public async Task<RespondListDto<ModelDisplayDto>> GetModels(SieveModel query)
        {
            int pageSize = query.PageSize != null ? (int)query.PageSize : 40;

            var models = await _modelRepository.GetModels(query);
            var modelsDto = _mapper.Map<List<ModelDisplayDto>>(models);

            RespondListDto<ModelDisplayDto> respondListDto = new RespondListDto<ModelDisplayDto>();
            respondListDto.Items = modelsDto;
            respondListDto.ItemsCount = await _modelRepository.GetModelsCount(query);
            respondListDto.PagesCount = (int)Math.Ceiling((double)respondListDto.ItemsCount / pageSize);

            return respondListDto;
        }

        public async Task<ModelDisplayDto> CreateModel(ModelCreateDto modelDto)
        {
            var model = _mapper.Map<Model>(modelDto);
            await _modelRepository.CreateModel(model);

            return _mapper.Map<ModelDisplayDto>(model);
        }

        public async Task<ModelDisplayDto> UpdateModel(ModelUpdateDto modelDto)
        {
            var model = _mapper.Map<Model>(modelDto);
            await _modelRepository.UpdateModel(model);

            return _mapper.Map<ModelDisplayDto>(model);
        }
    }
}
