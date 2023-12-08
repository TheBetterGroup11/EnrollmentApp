using EnrollmentApplication.Models;
using System.Data.SqlClient;

namespace EnrollmentApplication
{
    public class DataAccess
    {
        private readonly string _connectionString;
        public int SessionId { get; set; } = 123;
        public int SessionTerm { get; set; }
        public int TermId { get; set; } = 0;


        public DataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Student", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student
                        {
                            StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName"))
                        };

                        students.Add(student);
                    }
                }
            }

            return students;
        }

        public string CheckLogin(string fn, string ln, int id)
        {
            var student = new Student();
            student.FirstName = fn;
            student.LastName = ln;
            student.StudentId = id;
            string ret = "Query Error";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Student WHERE FirstName = @FirstName AND LastName = @LastName AND StudentId = @StudentId";
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@StudentId", student.StudentId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var searched = new Student
                        {
                            StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName"))
                        };

                        ret = "Valid";
                        SessionId = student.StudentId;
                    }
                    else
                    {
                        ret = "Invalid";
                    }
                }
            }

            return ret;
        }

        public string CheckSignUp(Student student)
        {

            string ret = "Query Error";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Student WHERE FirstName = @FirstName AND LastName = @LastName AND StudentId = @StudentId AND Grade = @Grade";
                var checkCommand = new SqlCommand(query, connection);

                checkCommand.Parameters.AddWithValue("@FirstName", student.FirstName);
                checkCommand.Parameters.AddWithValue("@LastName", student.LastName);
                checkCommand.Parameters.AddWithValue("@StudentId", student.StudentId);
                checkCommand.Parameters.AddWithValue("@Grade", student.Grade);

                if (checkCommand.ExecuteScalar() == null)
                {
                    var insertQuery = "INSERT INTO Student (FirstName, LastName, StudentId, Grade) VALUES (@FirstName, @LastName, @StudentId, @Grade)";
                    var insertCommand = new SqlCommand(insertQuery, connection);

                    insertCommand.Parameters.AddWithValue("@FirstName", student.FirstName);
                    insertCommand.Parameters.AddWithValue("@LastName", student.LastName);
                    insertCommand.Parameters.AddWithValue("@StudentId", student.StudentId);
                    insertCommand.Parameters.AddWithValue("@Grade", student.Grade);

                    int rowsFound = insertCommand.ExecuteNonQuery();

                    if (rowsFound > 0)
                    {
                        ret = "Valid";
                        SessionId = student.StudentId;
                    }
                    else
                    {
                        ret = "Invalid";
                    }
                }
                else
                {
                    ret = "Valid";
                    SessionId = student.StudentId;
                }
            }

            return ret;
        }

        public List<Course> GetStudentCourses(int id)
        {
            var courses = new List<Course>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT C.CourseId, C.[Name], C.DepartmentId, C.CreditHours\r\nFROM Course C\r\n\tINNER JOIN ScheduledCourse SC ON SC.CourseId = C.CourseId\r\n\tINNER JOIN Schedule S ON S.ScheduleId = SC.ScheduleId\r\nWHERE S.StudentId = @StudentId AND\r\n\t  SC.Status = 'In Progress';";
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@StudentId", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aCourse = new Course
                        {
                            CourseId = reader.GetInt32(reader.GetOrdinal("CourseId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            CreditHours = reader.GetInt32(reader.GetOrdinal("CreditHours"))
                        };

                        courses.Add(aCourse);
                    }
                }
            }
                return courses;
        }

        public Student SearchForAccount(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Student WHERE StudentId = @StudentId";
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@StudentId", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var searched = new Student
                        {
                            StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Grade = reader.GetString(reader.GetOrdinal("Grade"))
                        };

                        return searched;
                    }
                }
            }
            return null;
        }

        public int GetCompletedCredits(Student student)
        {
            int credits = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT SUM(C.CreditHours) AS TotalCreditHours FROM ScheduledCourse SC INNER JOIN Schedule S ON SC.ScheduleId = S.ScheduleId INNER JOIN Course C ON SC.CourseId = C.CourseId WHERE S.StudentId = @StudentId AND SC.Status = 'Complete';";
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@StudentId", SessionId);

                try
                {
                    credits = (int)command.ExecuteScalar();
                }
                catch
                {
                    credits = 0;
                }
            }
            return credits;
        }

        public List<Course> SessionCoursesMinus(List<Course> current, int id)
        {
            var newCurrent = new List<Course>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT C.CourseId, C.[Name], C.DepartmentId, C.CreditHours FROM Course C INNER JOIN ScheduledCourse SC ON SC.CourseId = C.CourseId INNER JOIN Schedule S ON S.ScheduleId = SC.ScheduleId WHERE S.StudentId = @StudentId AND C.CourseId != @CourseId AND TermId = @TermId;";
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@CourseId", id);
                command.Parameters.AddWithValue("@StudentId", SessionId);
                command.Parameters.AddWithValue("@TermId", SessionTerm);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var course = new Course
                        {
                            CourseId = reader.GetInt32(reader.GetOrdinal("CourseId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            CreditHours = reader.GetInt32(reader.GetOrdinal("CreditHours"))
                        };

                        newCurrent.Add(course);
                    }
                }
            }
            return newCurrent;
        }

        public List<PrettyCourses> GetPrettyCourses(List<Course> studentCourses)
        {
            List<PrettyCourses> prettyCoursesList = new List<PrettyCourses>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var course in studentCourses)
                {
                    // Assuming you have a method to get the department code by department ID
                    string departmentCode = GetDepartmentCode(course.DepartmentId, connection);

                    PrettyCourses prettyCourse = new PrettyCourses
                    {
                        // Assuming the CourseId is at least 3 digits and you want the last 3 digits
                        PrettyId = departmentCode + " " + course.CourseId.ToString().Substring(course.CourseId.ToString().Length - 3),
                        PrettyName = course.Name
                    };

                    prettyCoursesList.Add(prettyCourse);
                }
            }

            return prettyCoursesList;
        }

        private string GetDepartmentCode(int departmentId, SqlConnection connection)
        {
            string returnString = "";

            var query = "SELECT Code FROM Department WHERE DepartmentId = @DepartmentId";
            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DepartmentId", departmentId);
            
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    returnString = reader.GetString(reader.GetOrdinal("Code"));
                }
            }
            return returnString;
        }


    }
}
