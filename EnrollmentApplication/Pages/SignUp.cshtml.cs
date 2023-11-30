using EnrollmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnrollmentApplication.Pages
{
    public class Sign_UpModel : PageModel
    {
        private readonly DataAccess _dataAccessService;
        [BindProperty]
        public Student StudentSignUp { get; set; }

        public Sign_UpModel(DataAccess dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string result = _dataAccessService.CheckSignUp(StudentSignUp);

            if (result == "Valid")
            {
                return RedirectToPage("/MyTerm");
            }
            else
            {
                ModelState.AddModelError("", "Invalid sign in credentials.");

                return Page();
            }
            
        }
    }
}
