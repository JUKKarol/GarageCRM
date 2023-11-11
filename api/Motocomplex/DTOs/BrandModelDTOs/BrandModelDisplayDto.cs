using Motocomplex.DTOs.BrandDTOs;
using Motocomplex.DTOs.ModelDTOs;

namespace Motocomplex.DTOs.BrandModelDTOs
{
    public class BrandModelDisplayDto
    {
        public BrandDisplayDto Brand { get; set; }
        public List<ModelDetalisDto> Models { get; set; }
    }
}