using Microsoft.EntityFrameworkCore;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.Entities;
using Sieve.Models;
using Sieve.Services;

namespace Motocomplex.Data.Repositories.ModelRepository
{
    public class ModelRepository(
        MotocomplexContext _db,
        ISieveProcessor _sieveProcessor) : IModelRepository
    {
        public async Task<Model> GetModelById(Guid modelId)
        {
            return await _db.Models.Include(m => m.Brand).FirstOrDefaultAsync(m => m.Id == modelId);
        }

        public async Task<Model> GetModelByName(string modelName)
        {
            return await _db.Models.Include(m => m.Brand).FirstOrDefaultAsync(m => m.Name.ToLower() == modelName.ToLower());
        }

        public async Task<List<Model>> GetModels(SieveModel query)
        {
            var models = _db
                .Models
                .Include(m => m.Brand)
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