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
                new User { Email = "john.doe@gmail.com", Name = "John", Password = "hellojohn123", Registration_Time = DateTime.Now },
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
                new Folder { Name = "Vacation", Description = "Vacation.", Creation_date = DateTime.Now },
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
                new Picture { ImageLink = "test.png", Name = "Grandmas Cat", Context = "Took a picture of a cat.", Creation_date = DateTime.Now, Latitude = 123, Longitude = 123 }
            };

            // Add categories to the database context
            context.Picture.AddRange(Picture);

            // Save changes to the database
            context.SaveChanges();
        }
    }
}
