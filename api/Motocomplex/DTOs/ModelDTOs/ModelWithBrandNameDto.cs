namespace Motocomplex.DTOs.ModelDTOs
{
    public class ModelWithBrandNameDto
    {
        public Guid ModelId { get; set; }
        public DateTime ModelCreatedAt { get; set; }
        public DateTime ModelUpdatedAt { get; set; }
        public Guid BrandId { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
    }
}
