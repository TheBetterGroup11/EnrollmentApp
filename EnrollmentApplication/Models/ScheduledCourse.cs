namespace EnrollmentApplication.Models
{
    public class ScheduledCourse
    {
        public int ScheduledCourseId { get; set; }
        public int ScheduleId { get; set; }
        public int CourseId { get; set; }
        public string Status { get; set; }
    }
}
