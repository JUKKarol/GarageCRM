namespace Motocomplex.DTOs.ModelDTOs
{
    public class ModelDisplayDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid brandId { get; set; }
        public string Name { get; set; }
    }
}