namespace KooliProjekt.Data
{
    public class SeedData
    {
        public static void GenerateUsers(ApplicationDbContext context)
        {
            if (context.User.Any())
            {
                return;
            }

            var User = new[]
            {
                new User { Email = "jane.smith@yahoo.com", Name = "Jane", Password = "welcomeJane!", Registration_Time = DateTime.Now },
                new User { Email = "michael.brown@outlook.com", Name = "Michael", Password = "mikeSecure99", Registration_Time = DateTime.Now },
                new User { Email = "emily.white@gmail.com", Name = "Emily", Password = "passEm123", Registration_Time = DateTime.Now },
                new User { Email = "david.jones@hotmail.com", Name = "David", Password = "davidStrong1", Registration_Time = DateTime.Now },
                new User { Email = "sarah.taylor@aol.com", Name = "Sarah", Password = "sarah!safe88", Registration_Time = DateTime.Now },
                new User { Email = "chris.miller@gmail.com", Name = "Chris", Password = "ChrisMpass7", Registration_Time = DateTime.Now },
                new User { Email = "laura.wilson@yahoo.com", Name = "Laura", Password = "laura@1234", Registration_Time = DateTime.Now },
                new User { Email = "jason.moore@live.com", Name = "Jason", Password = "mooreJason21", Registration_Time = DateTime.Now },
                new User { Email = "olivia.thomas@outlook.com", Name = "Olivia", Password = "oliviaLove!", Registration_Time = DateTime.Now },
            };

            // Add categories to the database context
            context.User.AddRange(User);

            // Save changes to the database
            context.SaveChanges();
        }
        public static void GenerateFolders(ApplicationDbContext context)
        {
            if (context.Folder.Any())
            {
                return;
            }

            var Folder = new[]
            {
                new Folder { Name = "Work", Description = "Work files.", Creation_date = DateTime.Now },
                new Folder { Name = "Photos", Description = "Saved photos.", Creation_date = DateTime.Now },
                new Folder { Name = "Projects", Description = "My projects.", Creation_date = DateTime.Now },
                new Folder { Name = "Music", Description = "Favorite songs.", Creation_date = DateTime.Now },
                new Folder { Name = "Notes", Description = "Important notes.", Creation_date = DateTime.Now },
                new Folder { Name = "Books", Description = "E-books.", Creation_date = DateTime.Now },
                new Folder { Name = "Finance", Description = "Budgeting.", Creation_date = DateTime.Now },
                new Folder { Name = "Games", Description = "Game saves.", Creation_date = DateTime.Now },
                new Folder { Name = "Videos", Description = "Video clips.", Creation_date = DateTime.Now },
            };

            // Add categories to the database context
            context.Folder.AddRange(Folder);

            // Save changes to the database
            context.SaveChanges();
        }
        public static void GeneratePictures(ApplicationDbContext context)
        {
            if (context.Picture.Any())
            {
                return;
            }

            var Picture = new[]
            {
                new Picture { ImageLink = "beach.png", Name = "Beach Sunset", Context = "Beautiful sunset.", Creation_date = DateTime.Now, Latitude = 45, Longitude = -80 },
                new Picture { ImageLink = "forest.png", Name = "Green Forest", Context = "Hiking trip.", Creation_date = DateTime.Now, Latitude = 39, Longitude = -105 },
                new Picture { ImageLink = "dog.png", Name = "Happy Dog", Context = "Met a cute dog.", Creation_date = DateTime.Now, Latitude = 37, Longitude = -122 },
                new Picture { ImageLink = "mountain.png", Name = "Snowy Peak", Context = "Mountain view.", Creation_date = DateTime.Now, Latitude = 48, Longitude = -120 },
                new Picture { ImageLink = "city.png", Name = "City Lights", Context = "Night skyline.", Creation_date = DateTime.Now, Latitude = 40, Longitude = -74 },
                new Picture { ImageLink = "lake.png", Name = "Calm Lake", Context = "Peaceful water.", Creation_date = DateTime.Now, Latitude = 42, Longitude = -89 },
                new Picture { ImageLink = "car.png", Name = "Classic Car", Context = "Saw an old car.", Creation_date = DateTime.Now, Latitude = 35, Longitude = -115 },
                new Picture { ImageLink = "food.png", Name = "Tasty Pizza", Context = "Delicious meal.", Creation_date = DateTime.Now, Latitude = 50, Longitude = -90 },
                new Picture { ImageLink = "fireworks.png", Name = "New Year", Context = "Fireworks show.", Creation_date = DateTime.Now, Latitude = 38, Longitude = -77 },
            };

            // Add categories to the database context
            context.Picture.AddRange(Picture);

            // Save changes to the database
            context.SaveChanges();
        }
    }
}
