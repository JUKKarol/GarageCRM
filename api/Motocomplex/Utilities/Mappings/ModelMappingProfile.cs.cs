using AutoMapper;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.Entities;

namespace Motocomplex.Utilities.Mappings
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<Model, ModelDisplayDto>().AfterMap((src, dest) => dest.BrandName = src.Brand.Name);
            CreateMap<ModelCreateDto, Model>().ReverseMap();
            CreateMap<ModelUpdateDto, Model>().ReverseMap();
        }
    }
}