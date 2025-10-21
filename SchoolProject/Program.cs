using System;
using System.Collections.Generic;
using SchoolProject;

class Program
{
    static void Main()
    {
        // Connection string لقاعدة بيانات .mdf
        //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZBook\School.mdf;Integrated Security=True;Connect Timeout=30";

        string connectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ZBook\School.mdf;Integrated Security=True;Connect Timeout=30";


        

        List<CourseTeacher> courses = CourseTeacherRepository.GetCourses(connectionString);

        if (courses.Count == 0)
        {
            Console.WriteLine("No data found.");
            return;
        }
        //CourseTeacherRepository.PrintCourses(courses);

        //IQueries query = new Queries();
        //query.MaxYear(courses);
        //query.GroupByNameAndCourse(courses);
        //query.MinCreditHoursCourse(courses);
        //query.DistinctCoursesAndSubjects(courses);
        //query.TopScoringSubjectsPerYear(courses);
        //query.MostRegisteredCourse(courses);
        //query.PrintCoursesWithHours(courses);


        IQueries query = new Queries();
        

        IQueriesMenu menu = new QueriesMenu();
        menu.ShowMenu(courses, query);

        






        Console.ReadLine(); 
    }
}
