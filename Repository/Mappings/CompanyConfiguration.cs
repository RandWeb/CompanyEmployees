﻿using DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mappings;
internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(60);

        builder.Property(x => x.Address).IsRequired().HasMaxLength(60);

        builder.HasMany(x => x.Employees).WithOne().OnDelete(DeleteBehavior.Cascade);

        builder.HasData
        (
        new Company
        {
            Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
            Name = "IT_Solutions Ltd",
            Address = "583 Wall Dr. Gwynn Oak, MD 21207",
            Country = "USA"
        },
        new Company
        {
            Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
            Name = "Admin_Solutions Ltd",
            Address = "312 Forest Avenue, BF 923",
            Country = "USA"
        }
        );
    }
}

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Id).IsRequired();
        builder.HasData
                        (
                        new Employee
                        {
                            Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                            Name = "Sam Raiden",
                            Age = 26,
                            Position = "Software developer",
                        },
                        new Employee
                        {
                            Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                            Name = "Jana McLeaf",
                            Age = 30,
                            Position = "Software developer",
                        },
                        new Employee
                        {
                            Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                            Name = "Kane Miller",
                            Age = 35,
                            Position = "Administrator",
                        }
                        );
    }
}

