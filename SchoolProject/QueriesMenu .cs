using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SchoolProject
{
    using System.Collections.Generic;

    public interface IQueriesMenu
    {
        void ShowMenu(List<CourseTeacher> courses, IQueries query);
    }


    public class QueriesMenu : IQueriesMenu
    {
        public void ShowMenu(List<CourseTeacher> courses, IQueries query)
        {
            try
            {
                while (true)
                {
                    Console.Clear();

                    // عرض جميع البيانات أولاً
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("=== All Courses Data ===\n");
                    try
                    {
                        CourseTeacherRepository.PrintCourses(courses);
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Error displaying courses: {ex.Message}");
                    }
                    Console.WriteLine("\n=== Queries Menu ===");
                    Console.ResetColor();

                    // الأسئلة / الاستعلامات
                    var questions = new List<(string Question, Action Action)>
                {
                    ("Show Max Year of Courses?", () => SafeExecute(() => query.MaxYear(courses))),
                    ("Show Group by Student Name and Course?", () => SafeExecute(() => query.GroupByNameAndCourse(courses))),
                    ("Show Course with Min Credit Hours?", () => SafeExecute(() => query.MinCreditHoursCourse(courses))),
                    ("Show Distinct Courses and Subjects?", () => SafeExecute(() => query.DistinctCoursesAndSubjects(courses))),
                    ("Show Top Scoring Subjects per Year?", () => SafeExecute(() => query.TopScoringSubjectsPerYear(courses))),
                    ("Show Course with Most Registered Students?", () => SafeExecute(() => query.MostRegisteredCourse(courses))),
                    ("Print Courses with Credit Hours and Registered Students?", () => SafeExecute(() => query.PrintCoursesWithHours(courses)))
                };

                    // عرض القائمة
                    for (int i = 0; i < questions.Count; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"{i + 1}. {questions[i].Question}");
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{questions.Count + 1}. Exit");
                    Console.ResetColor();

                    Console.Write("\nSelect an option number: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int choice))
                    {
                        if (choice == questions.Count + 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThank you for using the program! Goodbye!");
                            Console.ResetColor();
                            break;
                        }
                        else if (choice >= 1 && choice <= questions.Count)
                        {
                            Console.Write("\nDo you want to execute this query? (Yes/No): ");
                            string answer = Console.ReadLine()?.Trim().ToLower();

                            if (answer == "yes" || answer == "y")
                            {
                                ShowLoading();

                                // تنفيذ الاستعلام
                                Thread queryThread = new Thread(new ThreadStart(questions[choice - 1].Action));
                                queryThread.Start();
                                queryThread.Join();

                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("\nPress any key to return to menu...");
                                Console.ResetColor();
                                Console.ReadKey();
                            }
                            else if (answer == "no" || answer == "n")
                            {
                                continue; // العودة للقائمة
                            }
                            else
                            {
                                ShowError("Invalid input! Returning to menu...");
                            }
                        }
                        else
                        {
                            ShowError("Invalid option! Try again.");
                        }
                    }
                    else
                    {
                        ShowError("Invalid input! Enter a number.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Unexpected error: {ex.Message}");
            }
        }

        private void ShowLoading()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\nLoading");

            Thread loadingThread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(300);
                }
            });

            loadingThread.Start();
            loadingThread.Join();

            Console.WriteLine();
            Console.ResetColor();
        }

        private void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Thread.Sleep(300);
        }

        private void SafeExecute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                ShowError($"Error executing query: {ex.Message}");
            }
        }
    }
}
