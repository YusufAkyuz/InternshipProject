using Emp.Entity.Entities;
using Emp.Entity.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emp.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.PhoneNumber).HasMaxLength(11);
        builder.Property(x => x.Email).HasMaxLength(100);
        builder.Property(x => x.Department).HasMaxLength(100);

        builder.HasData(new User()
        {
            Id = Guid.Parse("05625C09-4C13-42CA-A0D7-AB2D3465A65B"),
            Name = "Yusuf",
            LastName = "Akyüz",
            PhoneNumber = "05415125099",
            Email = "yusufakyus47@gmail.com",
            Department = "Back End Development",
            Salary = "40000",
            DateOfEntry = DateTime.UtcNow,
            IsDeleted = false,
            RoleOfEmp = Role.Employer
        });
    }
}