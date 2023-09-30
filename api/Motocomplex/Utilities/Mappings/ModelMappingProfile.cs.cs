using AutoMapper;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.Entities;

namespace Motocomplex.Utilities.Mappings
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<ModelDisplayDto, Model>().ReverseMap();
            CreateMap<ModelCreateDto, Model>().ReverseMap();
            CreateMap<ModelUpdateDto, Model>().ReverseMap();
        }
    }
}
