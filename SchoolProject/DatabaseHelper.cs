using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SchoolProject
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<CourseTeacher> GetCoursesWithTeachers()
        {
            var list = new List<CourseTeacher>();

            string query = "SELECT * FROM CoursesWithTeachersTable2";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new CourseTeacher
                        {
                            First_name = reader["First_name"].ToString(),
                            Last_name = reader["Last_name"].ToString(),
                            Course_Name = reader["Course_Name"].ToString(),
                            Description = reader["description"].ToString(),
                            Subject_name = reader["Subject_name"].ToString(),
                            Active = Convert.ToInt32(reader["Active"]),
                            Credit_Houers = Convert.ToDecimal(reader["Credit_Houers"]),
                            Number_Register = Convert.ToInt32(reader["Number_Register"]),
                            Year_name = reader["Year_name"].ToString(),
                            Degree = Convert.ToInt32(reader["Degree"])
                        };
                        list.Add(item);
                    }
                }
            }

            return list;
        }

    }
}
