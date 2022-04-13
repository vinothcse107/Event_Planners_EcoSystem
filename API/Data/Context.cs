using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
      public class Context : DbContext
      {
            public Context(DbContextOptions<Context> options) : base(options) { }
            public DbSet<User> Users { get; set; }
      }
}