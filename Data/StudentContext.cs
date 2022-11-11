using Microsoft.EntityFrameworkCore;
using StudentRegistryApp.Models;

namespace StudentRegistryApp.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
        }
        public DbSet<Student> students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new StudentEntityTypeConfiguration().Configure(modelBuilder.Entity<Student>());
        }
    }
}
