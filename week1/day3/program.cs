using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using University2.Models;

namespace University2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new UniversityContext())
            {
               
                context.Database.EnsureCreated();

                SeedData(context);

                Console.WriteLine("\n--homework 2--");
                Console.WriteLine("1=======================================================================");
                //1
                var ListAllCources = context.Courses.ToList();
                Console.WriteLine($"the total courses are listed: {ListAllCources.Count}");
                foreach (var course in ListAllCources)
                {
                    Console.WriteLine($"- {course.Title}");
                }
                Console.WriteLine("2=======================================================================");
                //2
                var GetAllAsignments = context.Assignments.Where(a => a.CourseId == 1).ToList();
                Console.WriteLine($"\nThe total assignments for course 1 are: {GetAllAsignments.Count}");
                foreach(var assignment in GetAllAsignments)
                {
                    Console.WriteLine($"- {assignment.AssignmentTitle} (Dued to {assignment.DueDate.ToString()})");
                }
                Console.WriteLine("3=======================================================================");
                //3
                var GetAllStudents = context.Users.Where(u => u.UserRole == Role.Student).ToList();
                Console.WriteLine($"Total Number of Students: {GetAllStudents.Count}");
                foreach(var student in GetAllStudents)
                {
                    Console.WriteLine($"the students First Name: {student.FirstName},\n  the student last name {student.LastName} \n");
                }
                Console.WriteLine("4=======================================================================");
                //4
                var assignmentComments = context.Comments
                    .Include(c => c.User)
                    .Where(c => c.AssignmentId == 2)
                    .ToList();
                foreach (var comment in assignmentComments)
                {
                    Console.WriteLine($"- {comment.User.FirstName}: {comment.CommentContent}");
                }
                Console.WriteLine("5=======================================================================");
                //5
                var GetmyGrades = context.Grades
                    .Include(g => g.Assignment)
                    .Where(g => g.StudentId == 1)
                    .ToList();
                Console.WriteLine("\nGrades for Student ID 1:");
                foreach (var grade in GetmyGrades)
                {
                    string letter = GetLetterGrade(grade.Score);
                    Console.WriteLine($"-{grade.Assignment.AssignmentTitle}: {grade.Score}/100 (Grade: {letter})");
                }
                Console.WriteLine("6=======================================================================");
                //6
                var assignmentDetails = context.Assignments
                    .Include(a => a.Course)
                    .ThenInclude(c => c.Teacher)
                    .ToList();
                Console.WriteLine($"\nAssignments with Teacher Details: ");
                foreach(var a in assignmentDetails)
                {
                    Console.WriteLine($"- {a.AssignmentTitle} (Course: {a.Course.Title}, Teacher: {a.Course.Teacher.FirstName} {a.Course.Teacher.LastName})"); 
                }
                Console.WriteLine("7=======================================================================");
                //7
                var avgGradesForEachCourse = context.Courses
                    .Select(c => new
                    {
                        CourseTitle = c.Title,
                        AvgGrade = c.Assignments
                        .SelectMany(
                            a => a.Grades).Average(g => (double?)g.Score) ?? 0
                    }).ToList();
                Console.WriteLine($"\nAverage Grades Per Course:");
                foreach (var avg in avgGradesForEachCourse)
                {
                    Console.WriteLine($"- {avg.CourseTitle}: {Math.Round(avg.AvgGrade, 2)}");
                }
                Console.WriteLine("8=======================================================================");
                //8
                double myGPA = CalculateStudentGPA(context, 1);
                Console.WriteLine($"\nGPA for Student ID 1: {myGPA}");
                Console.WriteLine("9=======================================================================");
                //9
                var studentToUpdate = context.Users.FirstOrDefault(u => u.FirstName == "Fawzy");
                if (studentToUpdate != null)
                {
                    studentToUpdate.UserRole = Role.Teacher;
                    context.SaveChanges();
                    Console.WriteLine($"- Updated role for {studentToUpdate.FirstName} to Teacher.");
                }
                Console.WriteLine("10=======================================================================");
                //10
                var commentToDelete = context.Comments.FirstOrDefault();
                if (commentToDelete != null)
                {
                    context.Comments.Remove(commentToDelete);
                    context.SaveChanges();
                    Console.WriteLine($"- Deleted a comment successfully.");
                }
                Console.WriteLine("Done, press esc");
                Console.ReadKey();
            }

            
        }
        static string GetLetterGrade(double percentage)
        {
            if (percentage >= 90) return "A";
            if (percentage >= 80) return "B";
            if (percentage >= 70) return "C";
            if (percentage >= 60) return "D";
            return "F";
        }
        static double CalculateStudentGPA(UniversityContext context, int studentId)
        {
            var studentGrades = context.Grades
        .Where(g => g.StudentId == studentId)
        .ToList();

            if (!studentGrades.Any()) return 0;

            double totalPoints = 0;

            foreach (var grade in studentGrades)
            {
                string letterGrade = GetLetterGrade(grade.Score);
                totalPoints += letterGrade switch
                {
                    "A" => 4.0,
                    "B" => 3.0,
                    "C" => 2.0,
                    "D" => 1.0,
                    _ => 0.0
                };
            }

            return Math.Round(totalPoints / studentGrades.Count, 2);
        }
        //seeder
        static void SeedData(UniversityContext context)
        {
            if (!context.Users.Any())
            {
                Console.WriteLine("(Seeding)...");

                var users = new List<User>
                {
                    new User { FirstName = "Motaz", LastName = "Al Masri", Email = "motaz@gmail.com", PhoneNumber = "0959493837", UserRole = Role.Student },
                    new User { FirstName = "Yehya", LastName = "Msouty", Email = "yehya@gmail.com", PhoneNumber = "0911111111", UserRole = Role.Student },
                    new User { FirstName = "Hiba", LastName = "Jazba", Email = "hiba@gmail.com", PhoneNumber = "0922222222", UserRole = Role.Student },
                    new User { FirstName = "Marah", LastName = "Aljumaat", Email = "marah@gmail.com", PhoneNumber = "0933333333", UserRole = Role.Student },
                    new User { FirstName = "Aya", LastName = "Jazba", Email = "aya@gmail.com", PhoneNumber = "0944444444", UserRole = Role.Student },
                    new User { FirstName = "Nawar", LastName = "Al Tibi", Email = "nawar@gmail.com", PhoneNumber = "0955555555", UserRole = Role.Student },
                    new User { FirstName = "Mehyar", LastName = "Khuder", Email = "mehyar@gmail.com", PhoneNumber = "0966666666", UserRole = Role.Student },
                    new User { FirstName = "Ahmad", LastName = "Khaled", Email = "ahmad@gmail.com", PhoneNumber = "0977777777", UserRole = Role.Student },
                    new User { FirstName = "Masa", LastName = "Hammoud", Email = "masa@gmail.com", PhoneNumber = "0988888888", UserRole = Role.Student },
                    new User { FirstName = "Zuhair", LastName = "Alhomsi", Email = "zuhair@gmail.com", PhoneNumber = "0999999999", UserRole = Role.Student },
                    new User { FirstName = "Ayman", LastName = "Durra", Email = "ayman@gmail.com", PhoneNumber = "0912345678", UserRole = Role.Student },
                    new User { FirstName = "Moaaz", LastName = "Zakaria", Email = "moaaz@gmail.com", PhoneNumber = "0923456789", UserRole = Role.Student },
                    new User { FirstName = "Fawzy", LastName = "Sukkar", Email = "fawzy@gmail.com", PhoneNumber = "0934567892", UserRole = Role.Student },

                    new User { FirstName = "Sami", LastName = "Teacher", Email = "sami@88nintey.com", PhoneNumber = "555-0201", UserRole = Role.Teacher },
                    new User { FirstName = "Feryal", LastName = "Teacher", Email = "feryal@88nintey.com", PhoneNumber = "555-0202", UserRole = Role.Teacher }
                };

                context.Users.AddRange(users);
                context.SaveChanges(); 

                var sami = context.Users.First(u => u.FirstName == "Sami");
                var feryal = context.Users.First(u => u.FirstName == "Feryal");

                var syllabi = new List<Syllabus>
                {
                    new Syllabus { Description = "SQL Basics" },
                    new Syllabus { Description = "OOP" },
                    new Syllabus { Description = "Code, Migrations, and DbContext." },
                    new Syllabus { Description = "RESTful APIs." },
                    new Syllabus { Description = "React.js Basics" }
                };
                context.Syllabus.AddRange(syllabi);
                context.SaveChanges();

                var courses = new List<Course>
                {
                    new Course { Title = "SQL", StartDate = new DateTime(2026, 1, 10), EndDate = new DateTime(2026, 3, 10), TeacherId = sami.Id, SyllabusId = syllabi[0].Id },
                    new Course { Title = "C#", StartDate = new DateTime(2026, 3, 15), EndDate = new DateTime(2026, 5, 15), TeacherId = sami.Id, SyllabusId = syllabi[1].Id },
                    new Course { Title = "Entity Framework", StartDate = new DateTime(2026, 5, 20), EndDate = new DateTime(2026, 6, 20), TeacherId = feryal.Id, SyllabusId = syllabi[2].Id },
                    new Course { Title = "Web API", StartDate = new DateTime(2026, 6, 25), EndDate = new DateTime(2026, 8, 25), TeacherId = feryal.Id, SyllabusId = syllabi[3].Id },
                    new Course { Title = "React", StartDate = new DateTime(2026, 9, 1), EndDate = new DateTime(2026, 11, 1), TeacherId = sami.Id, SyllabusId = syllabi[4].Id }
                };
                context.Courses.AddRange(courses);
                context.SaveChanges();

                var assignments = new List<Assignment>
                {
                    // SQL
                    new Assignment { AssignmentTitle = "SQL basics_1", Description = "sql basics_1 exam", DueDate = new DateTime(2026, 3, 15), CourseId = courses[0].Id },
                    new Assignment { AssignmentTitle = "SQL basics_2", Description = "sql basics_2 exam", DueDate = new DateTime(2026, 3, 18), CourseId = courses[0].Id },
                    new Assignment { AssignmentTitle = "joins", Description = "Inner joins", DueDate = new DateTime(2026, 3, 22), CourseId = courses[0].Id },
                    new Assignment { AssignmentTitle = "Grouping", Description = "Group by and Having", DueDate = new DateTime(2026, 3, 25), CourseId = courses[0].Id },
                    new Assignment { AssignmentTitle = "final", Description = "final exam in database", DueDate = new DateTime(2026, 3, 28), CourseId = courses[0].Id },
                    // C#
                    new Assignment { AssignmentTitle = "C# Basics", Description = "variables", DueDate = new DateTime(2026, 3, 18), CourseId = courses[1].Id },
                    new Assignment { AssignmentTitle = "loops", Description = "exam in loops", DueDate = new DateTime(2026, 3, 21), CourseId = courses[1].Id },
                    new Assignment { AssignmentTitle = "functions", Description = "call by value", DueDate = new DateTime(2026, 3, 25), CourseId = courses[1].Id },
                    new Assignment { AssignmentTitle = "OOP Concepts", Description = "Classes and Inheritance", DueDate = new DateTime(2026, 3, 29), CourseId = courses[1].Id },
                    new Assignment { AssignmentTitle = "Interfaces", Description = "Polymorphism", DueDate = new DateTime(2026, 4, 1), CourseId = courses[1].Id },
                    // Entity Framework
                    new Assignment { AssignmentTitle = "Setup DbContext", Description = "Initial setup", DueDate = new DateTime(2026, 5, 25), CourseId = courses[2].Id },
                    new Assignment { AssignmentTitle = "Migrations", Description = "Adding tables", DueDate = new DateTime(2026, 6, 1), CourseId = courses[2].Id },
                    new Assignment { AssignmentTitle = "Relationships", Description = "1-to-Many, Many-to-Many", DueDate = new DateTime(2026, 6, 8), CourseId = courses[2].Id },
                    new Assignment { AssignmentTitle = "Performance", Description = "Eager vs Lazy loading", DueDate = new DateTime(2026, 6, 15), CourseId = courses[2].Id },
                    new Assignment { AssignmentTitle = "Repository Pattern", Description = "Implementation", DueDate = new DateTime(2026, 6, 18), CourseId = courses[2].Id },
                    // Web API
                    new Assignment { AssignmentTitle = "First Endpoint", Description = "Hello World API", DueDate = new DateTime(2026, 7, 5), CourseId = courses[3].Id },
                    new Assignment { AssignmentTitle = "CRUD Operations", Description = "GET, POST, PUT, DELETE", DueDate = new DateTime(2026, 7, 15), CourseId = courses[3].Id },
                    new Assignment { AssignmentTitle = "Authentication", Description = "JWT Tokens", DueDate = new DateTime(2026, 8, 1), CourseId = courses[3].Id },
                    new Assignment { AssignmentTitle = "Middleware", Description = "Custom logging", DueDate = new DateTime(2026, 8, 10), CourseId = courses[3].Id },
                    new Assignment { AssignmentTitle = "Final API Project", Description = "Complete API", DueDate = new DateTime(2026, 8, 20), CourseId = courses[3].Id },
                    // React
                    new Assignment { AssignmentTitle = "JSX Basics", Description = "Rendering elements", DueDate = new DateTime(2026, 9, 10), CourseId = courses[4].Id },
                    new Assignment { AssignmentTitle = "Components", Description = "Props and State", DueDate = new DateTime(2026, 9, 20), CourseId = courses[4].Id },
                    new Assignment { AssignmentTitle = "Hooks", Description = "useEffect and useState", DueDate = new DateTime(2026, 10, 5), CourseId = courses[4].Id },
                    new Assignment { AssignmentTitle = "Routing", Description = "React Router DOM", DueDate = new DateTime(2026, 10, 15), CourseId = courses[4].Id },
                    new Assignment { AssignmentTitle = "Final App", Description = "Full SPA", DueDate = new DateTime(2026, 10, 25), CourseId = courses[4].Id }
                };
                context.Assignments.AddRange(assignments);
                context.SaveChanges();

                var comments = new List<Comment>
                {
                    new Comment { CommentContent = "Is this due on Friday?", CreatCourseDate = DateTime.Now, AssignmentId = assignments[0].Id, UserId = users[0].Id },
                    new Comment { CommentContent = "I need help with the diagram.", CreatCourseDate = DateTime.Now, AssignmentId = assignments[4].Id, UserId = users[1].Id },
                    new Comment { CommentContent = "Great assignment!", CreatCourseDate = DateTime.Now, AssignmentId = assignments[9].Id, UserId = users[2].Id },
                    new Comment { CommentContent = "Make sure to check your connection string.", CreatCourseDate = DateTime.Now, AssignmentId = assignments[14].Id, UserId = sami.Id },
                    new Comment { CommentContent = "Postman is giving me a 401 error.", CreatCourseDate = DateTime.Now, AssignmentId = assignments[19].Id, UserId = users[4].Id },
                    new Comment { CommentContent = "My components are not rendering.", CreatCourseDate = DateTime.Now, AssignmentId = assignments[24].Id, UserId = users[5].Id },
                    new Comment { CommentContent = "Remember the difference between LEFT and INNER join.", CreatCourseDate = DateTime.Now, AssignmentId = assignments[1].Id, UserId = feryal.Id },
                    new Comment { CommentContent = "Does C# OOP", CreatCourseDate = DateTime.Now, AssignmentId = assignments[6].Id, UserId = users[7].Id },
                    new Comment { CommentContent = "migration failed.", CreatCourseDate = DateTime.Now, AssignmentId = assignments[11].Id, UserId = users[8].Id },
                    new Comment { CommentContent = "Props drilling is confusing.", CreatCourseDate = DateTime.Now, AssignmentId = assignments[21].Id, UserId = users[9].Id }
                };
                context.Comments.AddRange(comments);
                context.SaveChanges();

                var students = context.Users.Where(u => u.UserRole == Role.Student).ToList();
                var allAssignments = context.Assignments.ToList();
                var grades = new List<Grade>();
                var random = new Random();

                foreach (var student in students)
                {
                    foreach (var assignment in allAssignments)
                    {
                        grades.Add(new Grade
                        {
                            StudentId = student.Id,
                            AssignmentId = assignment.Id,
                            Score = random.Next(55, 101) 
                        });
                    }
                }

                context.Grades.AddRange(grades);
                context.SaveChanges();

                Console.WriteLine("done Successfully");
            }
            else
            {
                Console.WriteLine("Already exists in database");
            }
        }
    }
}