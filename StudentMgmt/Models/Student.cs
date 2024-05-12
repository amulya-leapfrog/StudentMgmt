using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentMgmt.Enums;

namespace StudentMgmt.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Mobile number is required")]
        public string? Mobile { get; set; }

        public StudentType StudentType { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public Guid CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

    }
}