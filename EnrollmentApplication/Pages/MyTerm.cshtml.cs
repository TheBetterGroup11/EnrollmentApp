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
        public string courseName { get; set; } = "null";


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

        [HttpPost]
        public ActionResult ManageCourse(string courseName, string action)
        {
            if (action == "add")
            {
                
            }
            else if (action == "remove")
            {

            }

            return RedirectToPage();
        }

        public IActionResult OnPostAddCourse()
        {
            if (!string.IsNullOrEmpty(courseName))
            {
                // Call your method to add the course
                _dataAccessService.AddScheduledCourse(courseName);
                // Assuming AddCourse method updates StudentCourses and PrettyCourses internally
            }

            // Refresh the list of courses to reflect the changes
            StudentCourses = _dataAccessService.GetStudentCourses(_dataAccessService.SessionId);
            PrettyCourses = _dataAccessService.GetPrettyCourses(StudentCourses);

            return RedirectToPage();
        }

        public IActionResult OnPostRemoveCourse()
        {
            if (!string.IsNullOrEmpty(courseName))
            {
                // Call your method to remove the course
                _dataAccessService.DeleteScheduledCourse(courseName);
                // Assuming RemoveCourse method updates StudentCourses and PrettyCourses internally
            }

            // Refresh the list of courses to reflect the changes
            StudentCourses = _dataAccessService.GetStudentCourses(_dataAccessService.SessionId);
            PrettyCourses = _dataAccessService.GetPrettyCourses(StudentCourses);

            return RedirectToPage();
        }
    }
}
