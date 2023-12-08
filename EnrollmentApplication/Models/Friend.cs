namespace EnrollmentApplication.Models
{
    public class Friend
    {
        public int FriendId { get; set; }
        public int StudentId { get; set; }
        public int FriendStudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string Grade { get; set; }
    }
}
