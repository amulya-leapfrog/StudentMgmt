using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentMgmt.Enums;
using StudentMgmt.Models;
using System.Reflection;

namespace StudentMgmt.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var courseId = Guid.NewGuid();

            modelBuilder.Entity<Course>().HasData(
                   new Course { CourseId = courseId, Name = "Computer Science" },
                   new Course { CourseId = Guid.NewGuid(), Name = "Mathematics" }
                   );

            modelBuilder.Entity<Student>()
                         .Property(d => d.StudentType)
                         .HasConversion<String>();

            modelBuilder.Entity<Student>().HasData(new Student
            {
                StudentId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john@doe.com",
                Mobile = "9811532598",
                StudentType = Enums.StudentType.Old,
                CourseId = courseId
            });
        }
    }
}
