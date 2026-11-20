
using Microsoft.EntityFrameworkCore;
using LifeManagementApp.Models;

namespace LifeManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        // Database table for todos
        public DbSet<Todo> Todos { get; set; }

        // Constructor that receives database options
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
