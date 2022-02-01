using Microsoft.EntityFrameworkCore;
using todo_service_refwebsoftware.Models;

namespace todo_service_refwebsoftware.Data
{
    public class AppDbContext : DbContext
    {
        // Pont entre notre model et notre BDD
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){}

        public DbSet<Todo> todo  { get; set; }

        public DbSet<Specialization> specialization  { get; set; }
    
        public DbSet<User> user { get; set; }

        public DbSet<Project> project { get; set; }
    }
}