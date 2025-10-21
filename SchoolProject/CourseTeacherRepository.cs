using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject
{
    class CourseTeacherRepository
    {
        public static List<CourseTeacher> GetCourses(string connectionString)
        {
            var courses = new List<CourseTeacher>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //string query = @"SELECT 
                //                    t.First_name, 
                //                    t.Last_name, 
                //                    c.Course_Name, 
                //                    c.Description, 
                //                    s.Subject_name, 
                //                    c.Active, 
                //                    c.Credit_Houers, 
                //                    c.Number_Register, 
                //                    y.Year_name, 
                //                    c.Degree
                //                 FROM Courses c
                //                 JOIN Teachers t ON c.Teacher_Id = t.Id
                //                 JOIN Subjects s ON c.Subject_Id = s.Id
                //                 JOIN Years y ON c.Year_Id = y.Id";

                string query = "SELECT * FROM CoursesWithTeachersTable2";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new CourseTeacher
                        {
                            First_name = reader["First_name"].ToString(),
                            Last_name = reader["Last_name"].ToString(),
                            Course_Name = reader["Course_Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Subject_name = reader["Subject_name"].ToString(),
                            Active = Convert.ToInt32(reader["Active"]),
                            Credit_Houers = Convert.ToInt32(reader["Credit_Houers"]),
                            Number_Register = Convert.ToInt32(reader["Number_Register"]),
                            Year_name = reader["Year_name"].ToString(),
                            Degree = (int)(reader["Degree"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Degree"]))
                        });
                    }
                }
            }

            return courses;
        }

        public static void PrintCourses(IEnumerable<CourseTeacher> courses)
        {
            Console.WriteLine(
                "{0,-12} | {1,-12} | {2,-18} | {3,-25} | {4,-18} | {5,-6} | {6,-12} | {7,-10} | {8,-10} | {9,-7}",
                "First_name", "Last_name", "Course_Name", "Description", "Subject_name",
                "Active", "Credit_Hours", "Registered", "Year", "Degree");

            Console.WriteLine(new string('-', 160));

            foreach (var c in courses)
            {
                Console.WriteLine(
                    "{0,-12} | {1,-12} | {2,-18} | {3,-25} | {4,-18} | {5,-6} | {6,-12} | {7,-10} | {8,-10} | {9,-7}",
                    c.First_name, c.Last_name, c.Course_Name, c.Description, c.Subject_name,
                    c.Active, c.Credit_Houers, c.Number_Register, c.Year_name, c.Degree);
            }

            Console.WriteLine($"\nTotal ALL Courses: {courses.Count()}");
            Console.WriteLine("-------------"); 
        }
    }
}
