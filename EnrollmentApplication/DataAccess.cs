using EnrollmentApplication.Models;
using System.Data.SqlClient;

namespace EnrollmentApplication
{
    public class DataAccess
    {
        private readonly string _connectionString;
        private int _sessionId;
        public int SessionId {  get { return _sessionId; } }

        public DataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// All students in Student table
        /// </summary>
        /// <returns>List of all students in Student table</returns>
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
                        _sessionId = student.StudentId;
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
                        _sessionId = student.StudentId;
                    }
                    else
                    {
                        ret = "Invalid";
                    }
                }
                else
                {
                    ret = "Valid";
                    _sessionId = student.StudentId;
                }
            }

            return ret;
        }

        public List<Student> GetStudentCourses()
        {
            var students = new List<Student>();

            return students;
        }

        public Student SearchForAccount()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Student WHERE StudentId = @StudentId";
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@StudentId", _sessionId);

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

                command.Parameters.AddWithValue("@StudentId", _sessionId);

                credits = (int)command.ExecuteScalar();
            }
            return credits;
        }
    }
}
