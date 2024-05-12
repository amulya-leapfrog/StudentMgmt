using System.ComponentModel.DataAnnotations;

namespace StudentMgmt.Models
{
    public class Course
    {
        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
