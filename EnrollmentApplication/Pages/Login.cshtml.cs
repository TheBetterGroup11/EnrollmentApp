using EnrollmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnrollmentApplication.Pages
{
    public class LoginModel : PageModel
    {
        private readonly DataAccess _dataAccessService;

        [BindProperty]
        public Student StudentLogin { get; set; }

        public LoginModel(DataAccess dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string result = _dataAccessService.CheckLogin(StudentLogin.FirstName, StudentLogin.LastName, StudentLogin.StudentId);
        
            if (result == "Valid")
            {
                return RedirectToPage("/MyTerm");
            }

            ModelState.AddModelError("", "Invalid login credentials.");

            return Page();
        }
    }
}
