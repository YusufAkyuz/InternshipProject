namespace Emp.Entity.DTOs;

public class UserUpdateDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }
    public string Salary { get; set; }
    public DateTime DateOfEntry { get; set; }
    public bool IsDeleted { get; set; }
}