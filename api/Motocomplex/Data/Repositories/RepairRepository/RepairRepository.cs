using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;
using Motocomplex.Enums;
using Sieve.Models;
using Sieve.Services;

namespace Motocomplex.Data.Repositories.RepairRepository
{
    public class RepairRepository(
        MotocomplexContext _db,
        ISieveProcessor _sieveProcessor) : IRepairRepository
    {
        public async Task<Repair> GetRepairById(Guid repairId)
        {
            return await _db.Repairs.FirstOrDefaultAsync(r => r.Id == repairId);
        }

        public async Task<List<Repair>> GetRepairs(SieveModel query)
        {
            var repairs = _db
                .Repairs
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, repairs)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetRepairsCount(SieveModel query)
        {
            var repairs = _db
                .Repairs
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, repairs, applyPagination: false)
                .CountAsync();
        }

        public async Task<Repair> CreateRepair(Repair repair)
        {
            await _db.Repairs.AddAsync(repair);
            await _db.SaveChangesAsync();

            return repair;
        }

        public async Task<Repair> UpdateRepair(Repair updatedRepair)
        {
            var repair = await _db.Repairs.FirstOrDefaultAsync(r => r.Id == updatedRepair.Id);

            var repairCraetedAt = repair.CreatedAt;
            updatedRepair.CreatedAt = repairCraetedAt;

            var entry = _db.Entry(repair);
            entry.CurrentValues.SetValues(updatedRepair);

            await _db.SaveChangesAsync();

            return repair;
        }

        public async Task<Repair> UpdateRepairStatus(Guid repairId, RepairStatus repairStatus)
        {
            var repair = await _db.Repairs.FirstOrDefaultAsync(r => r.Id == repairId);
            repair.Status = repairStatus;
            await _db.SaveChangesAsync();

            return repair;
        }
    }
}