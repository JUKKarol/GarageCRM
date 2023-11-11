using Motocomplex.DTOs.BrandDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Sieve.Models;

namespace Motocomplex.Services.BrandService
{
    public interface IBrandService
    {
        Task<BrandDetalisDto> GetBrandById(Guid brandId);

        Task<BrandDetalisDto> GetBrandByName(string brandName);

        Task<RespondListDto<BrandDisplayDto>> GetBrands(SieveModel query);

        Task<BrandDisplayDto> CreateBrand(BrandCreateDto brandDto);

        Task<BrandDisplayDto> UpdateBrand(BrandUpdateDto brandDto);
    }
}