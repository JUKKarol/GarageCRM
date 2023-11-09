using Microsoft.EntityFrameworkCore;
using Motocomplex.Data.Repositories.ViewRepository;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.Entities;
using Sieve.Models;
using Sieve.Services;

namespace Motocomplex.Data.Repositories.ModelRepository
{
    public class ModelRepository : IModelRepository
    {
        private readonly MotocomplexContext _db;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IViewRepository _viewRepository;

        public ModelRepository(MotocomplexContext db, ISieveProcessor sieveProcessor, IViewRepository viewRepository)
        {
            _db = db;
            _sieveProcessor = sieveProcessor;
            _viewRepository = viewRepository;
        }

        public async Task<Model> GetModelById(Guid modelId)
        {
            return await _db.Models.FirstOrDefaultAsync(m => m.Id == modelId);
        }

        public async Task<ModelWithBrandNameDto> GetModelWithBrandNameById(Guid modelId)
        {
            var queryForView = _viewRepository.Select(_viewRepository.modelWithBrandName);

            var model = await _db.Database
                .SqlQuery<ModelWithBrandNameDto>(queryForView)
                .FirstOrDefaultAsync(m => m.ModelId == modelId);

            return model;
        }

        public async Task<Model> GetModelByName(string modelName)
        {
            return await _db.Models.FirstOrDefaultAsync(m => m.Name.ToLower() == modelName.ToLower());
        }

        public async Task<List<Model>> GetModels(SieveModel query)
        {
            var models = _db
                .Models
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, models)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetModelsCount(SieveModel query)
        {
            var models = _db
                .Models
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, models, applyPagination: false)
                .CountAsync();
        }

        public async Task<Model> CreateModel(Model model)
        {
            await _db.Models.AddAsync(model);
            await _db.SaveChangesAsync();

            return model;
        }

        public async Task<List<Model>> CreateModels(List<Model> models)
        {
            await _db.Models.AddRangeAsync(models);
            await _db.SaveChangesAsync();

            return models;
        }

        public async Task<Model> UpdateModel(Model updatedModel)
        {
            var model = await _db.Models.FirstOrDefaultAsync(m => m.Id == updatedModel.Id);

            var modelCraetedAt = model.CreatedAt;
            updatedModel.CreatedAt = modelCraetedAt;

            var entry = _db.Entry(model);
            entry.CurrentValues.SetValues(updatedModel);

            await _db.SaveChangesAsync();

            return model;
        }
    }
}