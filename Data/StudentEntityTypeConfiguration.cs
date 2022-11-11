using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentRegistryApp.Models;

namespace StudentRegistryApp.Data
{
    public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Surname).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Birthday).IsRequired();
            builder.Property(p => p.Birthplace).IsRequired().HasMaxLength(50);
        }
    }
}
