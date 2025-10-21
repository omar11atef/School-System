using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject
{
    public interface IQueries
    {
        void MaxYear(IEnumerable<CourseTeacher> courses);
        void GroupByNameAndCourse(IEnumerable<CourseTeacher> courses);
        void MinCreditHoursCourse(IEnumerable<CourseTeacher> courses);
        void DistinctCoursesAndSubjects(IEnumerable<CourseTeacher> courses);
        void TopScoringSubjectsPerYear(IEnumerable<CourseTeacher> courses);
        void MostRegisteredCourse(IEnumerable<CourseTeacher> courses);
        void PrintCoursesWithHours(IEnumerable<CourseTeacher> courses);
    }

    public class Queries : IQueries
    {
        public void MaxYear(IEnumerable<CourseTeacher> courses)
        {
            try
            {
                var query = courses.Max(c => c.Year_name);
                Console.WriteLine($"Max Year: {query}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MaxYear: {ex.Message}");
            }
        }

        public void GroupByNameAndCourse(IEnumerable<CourseTeacher> courses)
        {
            try
            {
                var getorder = courses.OrderByDescending(c => c.First_name).ThenBy(c => c.Last_name);
                var grouping = getorder.GroupBy(c => new { c.First_name, c.Last_name, c.Course_Name });
                foreach (var group in grouping)
                {
                    Console.WriteLine($"First-Name: {group.Key.First_name} {group.Key.Last_name}, Course: {group.Key.Course_Name}, Count: {group.Count()}");
                    CourseTeacherRepository.PrintCourses(group);
                    Console.WriteLine("--------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GroupByNameAndCourse: {ex.Message}");
            }
        }

        public void MinCreditHoursCourse(IEnumerable<CourseTeacher> courses)
        {
            try
            {
                var getorder = courses.OrderByDescending(c => c.First_name).ThenBy(c => c.Last_name);
                var minHouer = getorder.MinBy(c => c.Credit_Houers);
                Console.WriteLine("Min Number Houers in All Courses :");
                CourseTeacherRepository.PrintCourses(new List<CourseTeacher> { minHouer });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MinCreditHoursCourse: {ex.Message}");
            }
        }

        public void DistinctCoursesAndSubjects(IEnumerable<CourseTeacher> courses)
        {
            try
            {
                var getorder = courses.OrderByDescending(c => c.First_name).ThenBy(c => c.Last_name);
                var courseNames = getorder.Select(c => c.Course_Name).Distinct();
                Console.WriteLine($"Total Courses: {courseNames.Count()}");
                foreach (var subject in courseNames)
                    Console.WriteLine($"Course: {subject}");
                Console.WriteLine("\nWhat Lec in This Course :\n");

                var grouped = getorder.GroupBy(c => c.Course_Name);
                foreach (var group in grouped)
                {
                    Console.WriteLine($"Course Name: {group.Key}");
                    foreach (var item in group)
                        Console.WriteLine($"    Subject: {item.Subject_name}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DistinctCoursesAndSubjects: {ex.Message}");
            }
        }

        public void TopScoringSubjectsPerYear(IEnumerable<CourseTeacher> courses)
        {
            try
            {
                Console.WriteLine("Top Scoring Subjects per Year :");

                var topSubjects = courses
                    .GroupBy(
                        c => c.Year_name,
                        (key, group) => new
                        {
                            Year = key,
                            Subjects = group
                                .Where(c => c.Degree == group.Max(s => s.Degree))
                                .Select(s => new { s.Course_Name, s.Subject_name, s.Degree })
                                .Distinct(),
                            Count = group.Count()
                        }
                    );

                foreach (var yearGroup in topSubjects)
                {
                    Console.WriteLine($"\nYear: {yearGroup.Year} (Total Students: {yearGroup.Count})");
                    Console.WriteLine("{0,-20} | {1,-25} | {2,-6}",
                        "Course Name", "Subject Name", "Degree");
                    Console.WriteLine(new string('-', 60));

                    foreach (var s in yearGroup.Subjects)
                    {
                        Console.WriteLine("{0,-20} | {1,-25} | {2,-6}",
                            s.Course_Name,
                            s.Subject_name,
                            s.Degree);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in TopScoringSubjectsPerYear: {ex.Message}");
            }
        }

        public void MostRegisteredCourse(IEnumerable<CourseTeacher> courses)
        {
            try
            {
                var mostRegistered = courses
                    .GroupBy(c => c.Course_Name)
                    .Select(g => new
                    {
                        Course = g.Key,
                        TotalRegistered = g.Sum(c => c.Number_Register)
                    })
                    .OrderByDescending(c => c.TotalRegistered)
                    .First();

                Console.WriteLine($"Course with most registered students: {mostRegistered.Course} ({mostRegistered.TotalRegistered})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MostRegisteredCourse: {ex.Message}");
            }
        }

        public void PrintCoursesWithHours(IEnumerable<CourseTeacher> courses)
        {
            try
            {
                var getorder = courses.OrderByDescending(c => c.First_name).ThenBy(c => c.Last_name);

                var HouersInCourse = getorder.GroupBy(c => c.Course_Name, (key, group) => new
                {
                    Course = key,
                    Items = group
                });

                foreach (var courseGroup in HouersInCourse)
                {
                    Console.WriteLine($"\nCourse: {courseGroup.Course}");
                    Console.WriteLine("{0,-20} | {1,-15} | {2,-15}", "Credit Hours", "Registered", "Subject Name");
                    Console.WriteLine(new string('-', 55));

                    foreach (var item in courseGroup.Items)
                    {
                        Console.WriteLine("{0,-20} | {1,-15} | {2,-15}",
                            item.Credit_Houers,
                            item.Number_Register,
                            item.Subject_name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PrintCoursesWithHours: {ex.Message}");
            }
        }
    }
}
