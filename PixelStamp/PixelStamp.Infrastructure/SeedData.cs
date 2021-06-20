using System;
using System.Collections.Generic;
using PixelStamp.Core.Constants;
using PixelStamp.Core.Entities;
using PixelStamp.Infrastructure.DbContexts;

namespace PixelStamp.Infrastructure
{
    public class SeedData
    {
        private static DateTime now = DateTime.Now;

        private static bool IsDevelopment { get; set; } = true;

        /// <summary>
        /// Initialize the database.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="isDevelopment">Determine if this is the development environment.</param>
        public static void Initialize(CommandDbContext db, bool isDevelopment)
        {

            if (db is null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            IsDevelopment = isDevelopment;

            // Roles.
            db.Roles.AddRange(CreateRoles());

            db.Users.AddRange(CreatUsersWithRoles());

            db.Questions.AddRange(CreateQuestions());

            db.SaveChanges();
        }
       
        private static AppRole[] CreateRoles()
        {
            return new[]
            { 
                // teacher
                new AppRole{ Id = ApplicationRolesConstants.Teacher.Code, Name = ApplicationRolesConstants.Teacher.Name, NormalizedName = "TEACHER", ConcurrencyStamp = "8de18ffe-2ed8-4ec7-9fad-a847fc51636e"},
                // student
                new AppRole{ Id = ApplicationRolesConstants.Student.Code, Name = ApplicationRolesConstants.Student.Name, NormalizedName = "STUDENT",ConcurrencyStamp = "191c07fa-3f91-490b-bf8c-c95540b96b03"},
            };
        }

        private static AppUser[] CreatUsersWithRoles()  
        {
            var users = new List<AppUser>
            {
                // System Administrator
                new AppUser
                {
                    Id = new Guid(),
                    CreatedOn = now,
                    UserName="system",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Enabled = true
                }
            };

            if (IsDevelopment)
            {
                var devUsers = new[]
                {
                    // Teacher
                    new AppUser
                    {
                        Id = new Guid("f4dd3f12-d6ec-4e9c-a2b2-ed7cfae15c7b"),
                        CreatedOn = now,
                        UserName = "1111111111",
                        NormalizedUserName = "1111111111",
                        PasswordHash = "AQAAAAEAACcQAAAAEDN+G0Jfr6BWHPnNUAbu/8tlSkd1TsiBuYtyzlvO2EU3XqeoFQe+4KZ1dEoMDiGGbw==",
                        Email = "admin@moe.sa",
                        Enabled = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserRoles = new List<AppUserRole> { new AppUserRole { RoleId = ApplicationRolesConstants.Teacher.Code } },
                    },
                    // Student
                    new AppUser
                    {
                        Id = new Guid("c1cc4289-0e4a-4e6b-9b21-6b6c39841eb1"),
                        CreatedOn = now,
                        UserName = "1111111112",
                        NormalizedUserName = "1111111112",
                        PasswordHash = "AQAAAAEAACcQAAAAEDN+G0Jfr6BWHPnNUAbu/8tlSkd1TsiBuYtyzlvO2EU3XqeoFQe+4KZ1dEoMDiGGbw==",
                        Email = "GeneralSupervisor@moe.sa",
                        Enabled = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserRoles = new List<AppUserRole> { new AppUserRole { RoleId = ApplicationRolesConstants.Student.Code } },
                    },
                };

                users.AddRange(devUsers);
            }


            return users.ToArray();
        }
        public static Question[] CreateQuestions()
        {
            return new[] {
                new Question {Id=1,Text="Quest1"},
                new Question {Id=2,Text="Quest2"},
                new Question {Id=3,Text="Quest3"},
                new Question {Id=4,Text="Quest3"},
                new Question {Id=5,Text="Quest4"},
                new Question {Id=6,Text="Quest5"},
            };
        }

    }
}
