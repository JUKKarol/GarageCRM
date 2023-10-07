using Motocomplex.Entities;
using Motocomplex.Enums;
using Sieve.Models;

namespace Motocomplex.Data.Repositories.RepairRepository
{
    public interface IRepairRepository
    {
        Task<Repair> GetRepairById(Guid repairId);
        Task<List<Repair>> GetRepairs(SieveModel query);
        Task<int> GetRepairsCount(SieveModel query);
        Task<Repair> CreateRepair(Repair repair);
        Task<Repair> UpdateRepair(Repair updatedRepair);
        Task<Repair> UpdateRepairStatus(Guid repairId, RepairStatus repairStatus);
    }
}
