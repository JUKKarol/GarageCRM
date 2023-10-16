using AutoMapper;
using Motocomplex.Data.Repositories.BrandRepository;
using Motocomplex.Data.Repositories.ModelRepository;
using Motocomplex.DTOs.BrandModelDTOs;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Motocomplex.Entities;
using Sieve.Models;

namespace Motocomplex.Services.ModelService
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public ModelService(IModelRepository modelRepository, IBrandRepository brandRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<ModelDisplayDto> GetModelById(Guid modelId)
        {
            var model = await _modelRepository.GetModelById(modelId);
            return _mapper.Map<ModelDisplayDto>(model);
        }

        public async Task<ModelDisplayDto> GetModelByName(string modelName)
        {
            var model = await _modelRepository.GetModelByName(modelName);
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

        public async Task<List<ModelDisplayDto>> CreateMassModel(List<BrandModelCreateDto> brandModelDto)
        {
            var brands = await _brandRepository.CreateBrands(_mapper.Map<List<Brand>>(brandModelDto));
            var models = new List<Model>();

            foreach (var brandModel in brandModelDto)
            {
                var brand = brands.FirstOrDefault(b => b.Name == brandModel.BrandName);

                if (brand != null)
                {
                    var brandModels = brandModel.ModelNames.Select(modelName => new Model
                    {
                        brandId = brand.Id,
                        Name = modelName
                    });

                    models.AddRange(brandModels);
                }
            }

            return _mapper.Map<List<ModelDisplayDto>>(await _modelRepository.CreateModels(models));
        }

        public async Task<ModelDisplayDto> UpdateModel(ModelUpdateDto modelDto)
        {
            var model = _mapper.Map<Model>(modelDto);
            await _modelRepository.UpdateModel(model);

            return _mapper.Map<ModelDisplayDto>(model);
        }
    }
}