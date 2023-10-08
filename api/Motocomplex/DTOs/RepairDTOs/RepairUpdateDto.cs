namespace Motocomplex.DTOs.RepairDTOs
{
    public class RepairUpdateDto
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public Guid CarId { get; set; }
        public Guid CustomerId { get; set; }
        public List<Guid> EmployeesIds { get; set; }
    }
}
