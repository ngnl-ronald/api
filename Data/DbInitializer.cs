using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.Data
{
    public static class DbInitializer
    {
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static void Initialize(DataContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.AuthUsers.Any())
            {
                return;   // DB has been seeded
            }


            byte[] passwordHash, passwordSalt;
            var password = "masterkey";
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new AuthUser[]
            {
                new AuthUser
                    {
                        Username = "admin",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        SecurityUserRole_Id = 1
                    } // 1-admin
            };

            foreach (AuthUser s in user)
            {
                context.AuthUsers.Add(s);
            }
            context.SaveChanges();

          


            //var enrollments = new Enrollment[]
            //{
            //    new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Alexander").ID,
            //        CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
            //        Grade = Grade.A
            //    },
            //        new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Alexander").ID,
            //        CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
            //        Grade = Grade.C
            //        },
            //        new Enrollment {
            //        StudentID = students.Single(s => s.LastName == "Alexander").ID,
            //        CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
            //        Grade = Grade.B
            //        }
            //};

            //foreach (Enrollment e in enrollments)
            //{
            //    var enrollmentInDataBase = context.Enrollments.Where(
            //        s =>
            //                s.Student.ID == e.StudentID &&
            //                s.Course.CourseID == e.CourseID).SingleOrDefault();
            //    if (enrollmentInDataBase == null)
            //    {
            //        context.Enrollments.Add(e);
            //    }
            //}
            //context.SaveChanges();
        }


    }
}