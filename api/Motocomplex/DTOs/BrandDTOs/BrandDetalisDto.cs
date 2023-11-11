using Motocomplex.DTOs.ModelDTOs;

namespace Motocomplex.DTOs.BrandDTOs
{
    public class BrandDetalisDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }

        public List<ModelDisplayDto> Models { get; set; }
    }
}
