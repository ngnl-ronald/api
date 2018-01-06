using System;
using System.IO;
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
            context.Database.EnsureCreated();

            //===EXECUTE ALL SQL FILES IN root/Schema TO DB
            //===DEV ONLY
            var path = AppDomain.CurrentDomain.BaseDirectory;
            string schemaDir = Path.GetDirectoryName(path); //without file name
            schemaDir = Path.GetDirectoryName(schemaDir); // Temp folder
            schemaDir = Path.GetDirectoryName(schemaDir);
            schemaDir = Path.GetDirectoryName(schemaDir) + @"/Schema/";
            //string schemaFile = Path.GetDirectoryName(schemaDir) + @"/Schema/Functions/get_lookup.sql";

            string[] directories = Directory.GetDirectories(schemaDir);

            foreach (string s in directories)
            {
                string[] files = Directory.GetFiles(s);

                foreach (string a in files)
                {
                    if (a.Contains(".sql"))
                    {
                        string file = File.OpenText(a).ReadToEnd();
                        context.Database.ExecuteSqlCommand(file);
                    }

                }

            }

            //===END

            // Look for any students.
            if (context.auth_users.Any())
            {
                return;   // DB has been seeded
            }


            byte[] passwordHash, passwordSalt;
            var password = "masterkey";
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new auth_user[]
            {
                new auth_user
                    {
                        username = "admin",
                        password_hash = passwordHash,
                        password_salt = passwordSalt,
                        security_user_role_id = 1
                    } // 1-admin
            };

            foreach (auth_user s in user)
            {
                context.auth_users.Add(s);
            }

            var authUserRole = new auth_user_role[]
            {
               new auth_user_role { name = "Administrator"},
               new auth_user_role { name = "User" },
               new auth_user_role { name = "Employee" }
            };

            foreach (auth_user_role s in authUserRole)
            {
                context.auth_user_roles.Add(s);
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