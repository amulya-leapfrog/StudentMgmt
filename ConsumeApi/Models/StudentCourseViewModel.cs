namespace ConsumeApi.Models
{
    public class StudentCourseViewModel
    {
        public StudentViewModel? Student { get; set; }
        public List<CourseViewModel>? Course { get; set; } // Property for the course information

    }
}
