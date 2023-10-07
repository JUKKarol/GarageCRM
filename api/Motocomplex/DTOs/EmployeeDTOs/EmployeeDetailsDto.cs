using Motocomplex.Enums;

namespace Motocomplex.DTOs.EmployeeDTOs
{
    public class EmployeeDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public UserRole Role { get; set; }
    }
}