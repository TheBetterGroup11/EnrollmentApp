using EnrollmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnrollmentApplication.Pages
{
    public class My_TermModel : PageModel
    {
        private readonly DataAccess _dataAccessService;

        public Student _myTermStudent { get; set; }

        public List<Course> _studentCourses;
        public List<Course> StudentCourses
        {
            get { return _dataAccessService.GetStudentCourses(_dataAccessService.SessionId); }
            set { _studentCourses = value; }
        }
        public List<Course> RecommendedCourses { get; set; }

        public My_TermModel(DataAccess dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public void OnGet()
        {
            _myTermStudent = _dataAccessService.SearchForAccount(_dataAccessService._sessionId);
            
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
