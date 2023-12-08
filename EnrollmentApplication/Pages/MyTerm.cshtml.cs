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
        public List<PrettyCourses> PrettyCourses { get; set; }
        public List<Course> RecommendedCourses { get; set; }

        [BindProperty]
        public string newCourse { get; set; } = "null";


        public My_TermModel(DataAccess dataAccessService)
        {
            _dataAccessService = dataAccessService;
            StudentCourses = _dataAccessService.GetStudentCourses(_dataAccessService.SessionId);
        }

        public void OnGet()
        {
            SessionStudent = _dataAccessService.SearchForAccount(_dataAccessService.SessionId);
            PrettyCourses = _dataAccessService.GetPrettyCourses(StudentCourses);

            //RecommendedCourses = _dataAccessService.GetRecommendedCourses

        }

        public IActionResult OnPost(int courseId)
        {
            StudentCourses = _dataAccessService.SessionCoursesMinus(StudentCourses, courseId); // Assuming this method removes the course
            return RedirectToPage();
        }
    }
}
