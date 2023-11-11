using AutoMapper;
using Motocomplex.DTOs.BrandDTOs;
using Motocomplex.DTOs.BrandModelDTOs;
using Motocomplex.Entities;

namespace Motocomplex.Utilities.Mappings
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<BrandDisplayDto, Brand>().ReverseMap();
            CreateMap<Brand, BrandDetalisDto>().ForMember(dest => dest.Models, opt => opt.MapFrom(src => src.Models));
            CreateMap<BrandCreateDto, Brand>().ReverseMap();
            CreateMap<BrandUpdateDto, Brand>().ReverseMap();
            CreateMap<BrandModelCreateDto, Brand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BrandName))
            .ForMember(dest => dest.Models, opt => opt.Ignore()).ReverseMap();
        }
    }
}