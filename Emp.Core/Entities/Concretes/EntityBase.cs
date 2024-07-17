using System.ComponentModel.DataAnnotations;
using Emp.Entity.Enums;

namespace Emp.Core.Entities.Concretes;

public class EntityBase : IEntityBase
{
    [MaxLength(12)]
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }
    public string Salary { get; set; }
    public DateTime DateOfEntry { get; set; }
    public bool IsDeleted { get; set; }
    public Role RoleOfEmp { get; set; }
}