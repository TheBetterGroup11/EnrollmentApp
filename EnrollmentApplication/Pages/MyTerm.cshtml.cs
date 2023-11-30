using EnrollmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnrollmentApplication.Pages
{
    public class My_TermModel : PageModel
    {
        private readonly DataAccess _dataAccessService;

        public List<Course> StudentCourses { get; set; }
        public List<Course> RecommendedCourses { get; set; }

        public My_TermModel(DataAccess dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public void OnGet()
        {
            StudentCourses = _dataAccessService.GetStudentCourses();
        }
    }
}
