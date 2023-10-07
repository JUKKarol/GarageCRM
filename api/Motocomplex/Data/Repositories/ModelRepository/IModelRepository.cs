using Motocomplex.Entities;
using Sieve.Models;

namespace Motocomplex.Data.Repositories.ModelRepository
{
    public interface IModelRepository
    {
        Task<Model> GetModelById(Guid modelId);

        Task<Model> GetModelByNAme(string modelName);

        Task<List<Model>> GetModels(SieveModel query);

        Task<int> GetModelsCount(SieveModel query);

        Task<Model> CreateModel(Model model);

        Task<Model> UpdateModel(Model updatedModel);
    }
}