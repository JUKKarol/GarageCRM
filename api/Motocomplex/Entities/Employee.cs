using Motocomplex.Enums;

namespace Motocomplex.Entities
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfEmployment { get; set; } = DateTime.UtcNow;
        public UserRole Role { get; set; } = UserRole.Office;

        public List<Repair> Repairs { get; set; }
    }
}
