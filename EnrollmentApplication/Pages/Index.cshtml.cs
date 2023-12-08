using EnrollmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnrollmentApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataAccess _dataAccess;

        [BindProperty]
        public List<Student> Students { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _dataAccess = new DataAccess(configuration);
        }

        public void OnGet()
        {

        }
    }
}