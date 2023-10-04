using Motocomplex.Enums;

namespace Motocomplex.DTOs.EmployeeDTOs
{
    public class EmployeeUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfEmployment { get; set; } = DateTime.UtcNow;
        public UserRole Role { get; set; } = UserRole.Office;
    }
}
