namespace EnrollmentApplication.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int CreditHours { get; set; }
    }
}
