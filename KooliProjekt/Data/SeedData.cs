using KooliProjekt.Data;
using KooliProjekt.Models;
using System;
using System.Linq;

namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext dbContext)
        {
            // Seed Users if the table is empty
            if (!dbContext.Users.Any())
            {
                dbContext.Users.AddRange(
                    new Users { Email = "john.doe@gmail.com", Name = "John", Password = "hellojohn123", Registration_Time = DateTime.Now },
                    new Users { Email = "jane.smith@gmail.com", Name = "Jane", Password = "hellojane123", Registration_Time = DateTime.Now },
                    new Users { Email = "alice.jones@gmail.com", Name = "Alice", Password = "helloalice123", Registration_Time = DateTime.Now },
                    new Users { Email = "bob.martin@gmail.com", Name = "Bob", Password = "hellobob123", Registration_Time = DateTime.Now },
                    new Users { Email = "carol.white@gmail.com", Name = "Carol", Password = "hellocarol123", Registration_Time = DateTime.Now },
                    new Users { Email = "david.brown@gmail.com", Name = "David", Password = "hellodavid123", Registration_Time = DateTime.Now },
                    new Users { Email = "emily.green@gmail.com", Name = "Emily", Password = "helloemily123", Registration_Time = DateTime.Now },
                    new Users { Email = "frank.harris@gmail.com", Name = "Frank", Password = "hellofrank123", Registration_Time = DateTime.Now },
                    new Users { Email = "grace.king@gmail.com", Name = "Grace", Password = "hellograce123", Registration_Time = DateTime.Now },
                    new Users { Email = "henry.lee@gmail.com", Name = "Henry", Password = "hellohenry123", Registration_Time = DateTime.Now }
                );
            }

            // Seed Folders if the table is empty
            if (!dbContext.Folders.Any())
            {
                dbContext.Folders.AddRange(
                    new Folders { Name = "Vacation", Description = "Vacation pictures from 2023 trip.", Creation_date = DateTime.Now },
                    new Folders { Name = "Work", Description = "Work related pictures and documents.", Creation_date = DateTime.Now },
                    new Folders { Name = "Family", Description = "Family photos.", Creation_date = DateTime.Now },
                    new Folders { Name = "Friends", Description = "Friends gathering pictures.", Creation_date = DateTime.Now },
                    new Folders { Name = "Nature", Description = "Nature photography collection.", Creation_date = DateTime.Now }
                );
            }

            // Save all changes to the database
            dbContext.SaveChanges();
        }
    }
}
