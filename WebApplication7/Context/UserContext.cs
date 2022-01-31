using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> TodoItems { get; set; } = null!;
    }
}