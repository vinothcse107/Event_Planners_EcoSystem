using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
      public class Seed
      {
            public static void SeedUsers(Context context)
            {
                  if (context.Users.Any()) return;
                  var userData = System.IO.File.ReadAllText("Data/SeedData/UserSeed.json");
                  var users = JsonSerializer.Deserialize<List<User>>(userData);

                  foreach (var user in users)
                  {
                        Console.WriteLine(user.Username);
                        using var hmac = new HMACSHA512();
                        user.Username = user.Username.ToLower();
                        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));
                        user.PasswordSalt = hmac.Key;

                        context.Users.Add(user);
                  }
                  context.SaveChanges();
                  Console.WriteLine("Seeding Done");

            }
            public static void SeedHalls(Context context)
            {
                  if (context.Halls.Any()) return;
                  var HallData = System.IO.File.ReadAllText("Data/SeedData/HallsSeed.json");
                  var Halls = JsonSerializer.Deserialize<List<Hall>>(HallData);

                  foreach (var h in Halls)
                  {
                        context.Halls.Add(h);
                  }
                  context.SaveChanges();
                  Console.WriteLine("Halls Seeding Done");
            }

            public static void SeedPhotoGraphers(Context context)
            {
                  if (context.Photographers.Any()) return;
                  var Data = System.IO.File.ReadAllText("Data/SeedData/PhotoGrapherSeed.json");
                  var x = JsonSerializer.Deserialize<List<Photographer>>(Data);

                  foreach (var h in x)
                  {
                        context.Photographers.Add(h);
                  }
                  context.SaveChanges();
                  Console.WriteLine("Photographer Seeding Done");
            }

            public static void SeedCatering(Context context)
            {
                  if (context.Caterings.Any()) return;
                  var Data = System.IO.File.ReadAllText("Data/SeedData/CateringSeed.json");
                  var x = JsonSerializer.Deserialize<List<Catering>>(Data);

                  foreach (var h in x)
                  {
                        context.Caterings.Add(h);
                  }
                  context.SaveChanges();
                  Console.WriteLine("Catering Peoples added Seeding Done");
            }

            public static void SeedCateringItems(Context context)
            {
                  if (context.CateringFoodItems.Any()) return;
                  var Data = System.IO.File.ReadAllText("Data/SeedData/CateringFoodItemSeed.json");
                  var x = JsonSerializer.Deserialize<List<CateringFoodItem>>(Data);

                  foreach (var h in x)
                  {
                        context.CateringFoodItems.Add(h);
                  }
                  context.SaveChanges();
                  Console.WriteLine("Catering Items added Seeding Done");
            }

            public static void SeedEvents(Context context)
            {
                  if (context.Events.Any()) return;
                  var EventData = System.IO.File.ReadAllText("Data/SeedData/EventSeed.json");
                  var Event = JsonSerializer.Deserialize<List<Event>>(EventData);

                  foreach (var e in Event)
                  {
                        context.Events.Add(e);
                  }
                  context.SaveChanges();
                  Console.WriteLine("Event Seeding Done");
            }
            public static void SeedReviews(Context context)
            {
                  if (context.Reviews.Any()) return;
                  var ReviewsData = System.IO.File.ReadAllText("Data/SeedData/ReviewsSeed.json");
                  var Reviews = JsonSerializer.Deserialize<List<Review>>(ReviewsData);

                  foreach (var r in Reviews)
                  {
                        context.Reviews.Add(r);
                  }
                  context.SaveChanges();
                  Console.WriteLine("Reviews Seeding Done");
            }

      }
}



// [
//       {
//             "EventName": "eiusmod labore qui",
//             "EventTime": "2024-01-03T08:50:09",
//             "User_ID": "vinosiva",
//             "Hall_ID": "1B71E1CF-55FF-4B6D-D06B-08DA2AB4423E",
//             "PhotoGrapherID": "Combs",
//             "CateringId": "Flynn"
//       }
// ]
