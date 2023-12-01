using EnrollmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnrollmentApplication.Pages
{
    public class My_TermModel : PageModel
    {
        private readonly DataAccess _dataAccessService;

        public Student _myTermStudent { get; set; }
        public List<Course> StudentCourses { get; set; }
        public List<Course> RecommendedCourses { get; set; }

        public My_TermModel(DataAccess dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public void OnGet()
        {
            _myTermStudent = _dataAccessService.SearchForAccount(_dataAccessService._sessionId);
            StudentCourses = _dataAccessService.GetStudentCourses(_dataAccessService._sessionId);
            //RecommendedCourses = _dataAccessService.GetRecommendedCourses();
            RecommendedCourses = StudentCourses;

        }

        public IActionResult OnPostRemoveCourse(int courseId)
        {
            //_dataAccessService.RemoveCourse(courseId); // Assuming this method removes the course
            return RedirectToPage();
        }
    }
}
