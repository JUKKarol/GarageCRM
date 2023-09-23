namespace Motocomplex.Entities
{
    public class Brand
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }

        public List<Model> Models { get; set; }
        public List<Car> Cars { get; set; }
    }
}
