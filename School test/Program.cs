using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using School_test.Moduls;

namespace School_test
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathFile = "E:\\Javapj\\School test\\Data.txt";
            List<Student> students = new List<Student>();
            List<Parenet> parents = new List<Parenet>();

            Console.WriteLine("------- HELLO IN OUR SITE '_' ------");
            bool running = true;

            Console.WriteLine("=== Person Details App ===");
            while (running)
            {
                Console.WriteLine("\n[1] Add Person");
                Console.WriteLine("[2] Search Details");
                Console.WriteLine("[3] Exit");
                Console.Write("Select an option [1 - 3]: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Student person = new Student();
                    Parenet person2 = new Parenet();

                    int studentCount = GetStudentCount(pathFile);
                    int newID = studentCount + 1;

                    Console.WriteLine("****** Student Form ****** : ");
                    Console.Write($"Enter Your ID: ");
                    person.id = int.Parse(Console.ReadLine());
                    Console.Write($"Enter Your First name: ");
                    person.First_Name = Console.ReadLine();
                    Console.Write($"Enter Your Last name: ");
                    person.Last_Name = Console.ReadLine();
                    Console.Write($"Enter Your Age : ");
                    person.Age = int.Parse(Console.ReadLine());
                    Console.Write($"Enter Your phone name : ");
                    person.phone = Console.ReadLine();
                    Console.Write($"Enter Your Stage name : ");
                    person.Stage = Console.ReadLine();
                    Console.Write($"Enter Your City name : ");
                    person.City = Console.ReadLine();
                    Console.Write($"Enter Your Address name : ");
                    person.Address = Console.ReadLine();

                    students.Add(person);

                    using (StreamWriter writer = new StreamWriter(pathFile, true))
                    {
                        writer.WriteLine($"Student {newID}:");
                        writer.WriteLine($"ID : {person.id}");
                        writer.WriteLine($"First name: {person.First_Name}");
                        writer.WriteLine($"Last name: {person.Last_Name}");
                        writer.WriteLine($"Age: {person.Age}");
                        writer.WriteLine($"phone: {person.phone}");
                        writer.WriteLine($"Stage: {person.Stage}");
                        writer.WriteLine($"City: {person.City}");
                        writer.WriteLine($"Address: {person.Address}");
                    }
                    Console.WriteLine($"Student {newID} added and saved to file!");

                    Console.WriteLine("****** Parent Form ****** : ");
                    Console.Write($"Enter Your ID: ");
                    person2.id = int.Parse(Console.ReadLine());
                    Console.Write($"Enter Your First name: ");
                    person2.First_Name = Console.ReadLine();
                    Console.Write($"Enter Your Last name: ");
                    person2.Last_Name = Console.ReadLine();
                    Console.Write($"Enter Your Age : ");
                    person2.Age = int.Parse(Console.ReadLine());
                    Console.Write($"Enter Your Phone : ");
                    person2.phone = Console.ReadLine();
                    Console.Write($"Enter Your Email : ");
                    person2.Email = Console.ReadLine();
                    Console.Write($"Enter Your City : ");
                    person2.city = Console.ReadLine();

                    parents.Add(person2);

                    using (StreamWriter writer = new StreamWriter(pathFile, true))
                    {
                        writer.WriteLine("---");
                        writer.WriteLine($"Parent {newID}:");
                        writer.WriteLine($"ID : {person2.id}");
                        writer.WriteLine($"First name: {person2.First_Name}");
                        writer.WriteLine($"Last name: {person2.Last_Name}");
                        writer.WriteLine($"Age: {person2.Age}");
                        writer.WriteLine($"phone: {person2.phone}");
                        writer.WriteLine($"Stage: {person2.Email}");
                        writer.WriteLine($"Address: {person2.city}");
                        writer.WriteLine("--------------------------------------------------");
                    }
                    Console.WriteLine($"Parent {newID} added and saved to file!");
                }
                else if (choice == "2")
                {
                    Console.Write("Enter your ID: ");
                    if (int.TryParse(Console.ReadLine(), out int inputId))
                    {
                        var result = from s in students
                                     join p in parents on s.id equals p.id
                                     where s.id == inputId
                                     select new
                                     {
                                         StudentName = s.First_Name + " " + s.Last_Name,
                                         ParentName = p.First_Name + " " + p.Last_Name
                                     };

                        var match = result.FirstOrDefault();
                        if (match != null)
                        {
                            Console.WriteLine($"Student Name IS: {match.StudentName}, Parent Name IS: {match.ParentName}");
                        }
                        else
                        {
                            Console.WriteLine("No matching student and parent found for the given ID.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID entered.");
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Exiting program...");
                    running = false;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please choose 1 to 3.");
                }
            }
        }

        static int GetStudentCount(string pathFile)
        {
            if (!File.Exists(pathFile))
                return 0;

            int count = 0;
            using (StreamReader reader = new StreamReader(pathFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Student "))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}




      













