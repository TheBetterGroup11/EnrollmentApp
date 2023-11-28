using EnrollmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnrollmentApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataAccess _dataAccess;

        public List<Student> Students { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _dataAccess = new DataAccess(configuration);
        }

        public void OnGet()
        {

        }

        public IActionResult OnGetFetchStudents()
        {
            Students = _dataAccess.GetAllStudents(); // Fetch students
            return Page(); // Refresh the page with the new data
        }
    }
}