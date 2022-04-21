using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
      public class Context : DbContext
      {
            public Context(DbContextOptions<Context> options) : base(options) { }
            public DbSet<User> Users { get; set; }
            public DbSet<Event> Events { get; set; }
            public DbSet<Hall> Halls { get; set; }
            public DbSet<Review> Reviews { get; set; }


      }
}