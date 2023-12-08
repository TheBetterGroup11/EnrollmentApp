using EnrollmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnrollmentApplication.Pages
{
    public class FriendsModel : PageModel
    {
        private readonly DataAccess _dataAccessService;
        public List<Friend> StudentFriends { get; set; }

        public FriendsModel(DataAccess dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public void OnGet()
        {
            StudentFriends = _dataAccessService.GetStudentFriends();
        }
    }
}
