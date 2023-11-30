using EnrollmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnrollmentApplication.Pages
{
    public class Sign_UpModel : PageModel
    {
        private readonly DataAccess _dataAccessService;

        public Student StudentSignUp { get; set; }

        public Sign_UpModel(DataAccess dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            _dataAccessService.CheckLogin(StudentSignUp.FirstName, StudentSignUp.LastName, StudentSignUp.StudentId);
        }
    }
}
