using Motocomplex.DTOs.ModelDTOs;

namespace Motocomplex.DTOs.CarDTOs
{
    public class CarDetailsDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int Engine { get; set; }
        public string RegistrationNumber { get; set; }
        public string Vin { get; set; }
        public int yearOfProduction { get; set; }

        public ModelDetailsDto Model { get; set; }
    }
}
