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
                    new Folders { Name = "Nature", Description = "Nature photography collection.", Creation_date = DateTime.Now },
                    new Folders { Name = "Music", Description = "Collection of music albums.", Creation_date = DateTime.Now },
                    new Folders { Name = "Recipes", Description = "Favorite recipes and cooking tips.", Creation_date = DateTime.Now },
                    new Folders { Name = "Holidays", Description = "Holidays celebration photos and memories.", Creation_date = DateTime.Now },
                    new Folders { Name = "Pets", Description = "Cute pictures and videos of pets.", Creation_date = DateTime.Now },
                    new Folders { Name = "Travel", Description = "Travel destinations and experiences.", Creation_date = DateTime.Now },
                    new Folders { Name = "Fitness", Description = "Fitness and workout routines.", Creation_date = DateTime.Now },
                    new Folders { Name = "Books", Description = "Books I've read or want to read.", Creation_date = DateTime.Now },
                    new Folders { Name = "Tech", Description = "Technology-related articles and resources.", Creation_date = DateTime.Now },
                    new Folders { Name = "Gaming", Description = "Gaming screenshots and videos.", Creation_date = DateTime.Now },
                    new Folders { Name = "Art", Description = "Artworks and sketches.", Creation_date = DateTime.Now }
                );
            }

            // Seed Pictures if the table is empty
            if (!dbContext.Pictures.Any())
            {
                dbContext.Pictures.AddRange(
                    new Pictures { ImageLink = "cat.png", Name = "Grandmas Cat", Context = "Took a picture of a cat.", Creation_date = DateTime.Now, Latitude = 123, Longitude = 123 },
                    new Pictures { ImageLink = "dog.png", Name = "Fluffy Dog", Context = "A photo of a fluffy dog at the park.", Creation_date = DateTime.Now, Latitude = 45.678, Longitude = 89.123 },
                    new Pictures { ImageLink = "sunset.jpg", Name = "Sunset View", Context = "Captured a beautiful sunset on the beach.", Creation_date = DateTime.Now, Latitude = 32.987, Longitude = 103.543 },
                    new Pictures { ImageLink = "mountain.jpg", Name = "Mountain Hike", Context = "Hiking in the mountains.", Creation_date = DateTime.Now, Latitude = 50.676, Longitude = 30.443 },
                    new Pictures { ImageLink = "city.jpg", Name = "City Skyline", Context = "Skyline of the city at night.", Creation_date = DateTime.Now, Latitude = 40.712, Longitude = -74.006 },
                    new Pictures { ImageLink = "beach.png", Name = "Beach Relaxation", Context = "A relaxing day at the beach.", Creation_date = DateTime.Now, Latitude = 28.703, Longitude = 85.249 },
                    new Pictures { ImageLink = "flowers.jpg", Name = "Spring Flowers", Context = "Close-up of flowers in spring.", Creation_date = DateTime.Now, Latitude = 52.205, Longitude = 13.405 },
                    new Pictures { ImageLink = "restaurant.jpg", Name = "Restaurant Meal", Context = "Delicious meal at a local restaurant.", Creation_date = DateTime.Now, Latitude = 41.878, Longitude = -87.629 },
                    new Pictures { ImageLink = "concert.jpg", Name = "Live Concert", Context = "Exciting concert with a famous band.", Creation_date = DateTime.Now, Latitude = 51.507, Longitude = -0.128 },
                    new Pictures { ImageLink = "waterfall.jpg", Name = "Waterfall Adventure", Context = "Exploring a beautiful waterfall in the forest.", Creation_date = DateTime.Now, Latitude = 39.101, Longitude = 84.202 },
                    new Pictures { ImageLink = "garden.jpg", Name = "Secret Garden", Context = "Hidden garden with lush greenery.", Creation_date = DateTime.Now, Latitude = 33.525, Longitude = -118.245 },
                    new Pictures { ImageLink = "festival.jpg", Name = "Cultural Festival", Context = "Cultural festival with traditional dances.", Creation_date = DateTime.Now, Latitude = 48.856, Longitude = 2.352 },
                    new Pictures { ImageLink = "forest.jpg", Name = "Forest Trail", Context = "A walk through the dense forest trail.", Creation_date = DateTime.Now, Latitude = 47.396, Longitude = 123.562 }
                );
            }

            // Save all changes to the database
            dbContext.SaveChanges();
        }
    }
}
