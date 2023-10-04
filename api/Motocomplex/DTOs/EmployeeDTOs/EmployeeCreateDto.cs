using Motocomplex.Enums;

namespace Motocomplex.DTOs.EmployeeDTOs
{
    public class EmployeeCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfEmployment { get; set; } = DateTime.UtcNow;
        public UserRole Role { get; set; } = UserRole.Office;
    }
}
