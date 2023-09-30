using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;
using Sieve.Models;
using Sieve.Services;

namespace Motocomplex.Data.Repositories.ModelRepository
{
    public class ModelRepository : IModelRepository
    {
        private readonly MotocomplexContext _db;
        private readonly ISieveProcessor _sieveProcessor;

        public ModelRepository(MotocomplexContext db, ISieveProcessor sieveProcessor)
        {
            _db = db;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Model> GetModelById(Guid modelId)
        {
            return await _db.Models.FirstOrDefaultAsync(c => c.Id == modelId);
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

        public async Task<Model> UpdateModel(Model updatedModel)
        {
            var model = await _db.Models.FirstOrDefaultAsync(c => c.Id == updatedModel.Id);

            var modelCraetedAt = model.CreatedAt;
            updatedModel.CreatedAt = modelCraetedAt;

            var entry = _db.Entry(model);
            entry.CurrentValues.SetValues(updatedModel);

            await _db.SaveChangesAsync();

            return model;
        }
    }
}
