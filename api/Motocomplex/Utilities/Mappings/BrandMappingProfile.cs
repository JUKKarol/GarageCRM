using AutoMapper;
using Motocomplex.DTOs.BrandDTOs;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.Entities;

namespace Motocomplex.Utilities.Mappings
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<BrandDisplayDto, Brand>().ReverseMap();
            CreateMap<BrandCreateDto, Brand>().ReverseMap();
            CreateMap<BrandUpdateDto, Brand>().ReverseMap();
        }
    }
}
