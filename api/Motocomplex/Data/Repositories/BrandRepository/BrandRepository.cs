using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;
using Sieve.Models;
using Sieve.Services;

namespace Motocomplex.Data.Repositories.BrandRepository
{
    public class BrandRepository(
        MotocomplexContext _db,
        ISieveProcessor _sieveProcessor) : IBrandRepository
    {
        public async Task<Brand> GetBrandById(Guid brandId)
        {
            return await _db.Brands.Include(b => b.Models).FirstOrDefaultAsync(b => b.Id == brandId);
        }

        public async Task<Brand> GetBrandByName(string brandName)
        {
            return await _db.Brands.Include(b => b.Models).FirstOrDefaultAsync(b => b.Name.ToLower() == brandName.ToLower());
        }

        public async Task<List<Brand>> GetBrands(SieveModel query)
        {
            var brands = _db
                .Brands
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, brands)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetBrandsCount(SieveModel query)
        {
            var brands = _db
                .Brands
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, brands, applyPagination: false)
                .CountAsync();
        }

        public async Task<Brand> CreateBrand(Brand brand)
        {
            await _db.Brands.AddAsync(brand);
            await _db.SaveChangesAsync();

            return brand;
        }

        public async Task<List<Brand>> CreateBrands(List<Brand> brands)
        {
            await _db.Brands.AddRangeAsync(brands);
            await _db.SaveChangesAsync();

            return brands;
        }

        public async Task<Brand> UpdateBrand(Brand updatedBrand)
        {
            var brand = await _db.Brands.FirstOrDefaultAsync(b => b.Id == updatedBrand.Id);

            var brandCraetedAt = brand.CreatedAt;
            updatedBrand.CreatedAt = brandCraetedAt;

            var entry = _db.Entry(brand);
            entry.CurrentValues.SetValues(updatedBrand);

            await _db.SaveChangesAsync();

            return brand;
        }
    }
}