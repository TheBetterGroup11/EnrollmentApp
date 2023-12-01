using EnrollmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnrollmentApplication.Pages
{
    public class My_TermModel : PageModel
    {
        private readonly DataAccess _dataAccessService;

        public Student SessionStudent { get; set; }

        public List<Course> StudentCourses { get; set; }
        public List<Course> RecommendedCourses { get; set; }

        public My_TermModel(DataAccess dataAccessService)
        {
            _dataAccessService = dataAccessService;
            StudentCourses = _dataAccessService.GetStudentCourses(_dataAccessService.SessionId);
        }

        public void OnGet()
        {
            SessionStudent = _dataAccessService.SearchForAccount(_dataAccessService.SessionId);
            
            //RecommendedCourses = _dataAccessService.GetRecommendedCourses();
            RecommendedCourses = StudentCourses;

        }

        public IActionResult OnPostRemoveCourse(int courseId)
        {
            StudentCourses = _dataAccessService.SessionCoursesMinus(StudentCourses, courseId); // Assuming this method removes the course
            return RedirectToPage();
        }
    }
}
