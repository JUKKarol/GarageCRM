using Motocomplex.Enums;

namespace Motocomplex.DTOs.RepairDTOs
{
    public class RepairDisplayDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }
        public List<Guid> EmployeesIds { get; set; }
        public RepairStatus Status { get; set; }
    }
}