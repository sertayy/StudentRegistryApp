using Microsoft.EntityFrameworkCore;
using StudentRegistryApp.Models;
using System;

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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString")); // Böyle yapmak daha mantıklı değil mi? Startup'da set etmek yerine cünkü farklı farklı db contextler farklı db lere baglanabilir.
            base.OnConfiguring(optionsBuilder);
        }
    }
}
