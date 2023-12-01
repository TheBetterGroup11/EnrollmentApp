using EnrollmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnrollmentApplication.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly DataAccess _dataAccessService;

        [BindProperty]
        public Student CurrentStudent { get; set; }

        public string _fullName {  get; set; }
        public int _studentID { get; set; }
        public string _grade {  get; set; }
        public int _credits {  get; set; }

        public ProfileModel(DataAccess dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public void OnGet()
        {
            Student s = _dataAccessService.SearchForAccount(_dataAccessService._sessionId);
            _fullName = s.FirstName + s.LastName;
            _studentID = s.StudentId;
            _grade = s.Grade;
            _credits = _dataAccessService.GetCompletedCredits(s);
        }
    }
}
