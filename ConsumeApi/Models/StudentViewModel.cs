using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StudentMgmt.Enums;
using System.ComponentModel;

namespace ConsumeApi.Models
{
    public class StudentViewModel
    {
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }
       public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public StudentType StudentType { get; set; }

        public Guid CourseId { get; set; }
        public CourseViewModel? Course { get; set; } // Property for the course information

    }
}
