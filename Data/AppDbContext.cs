using Microsoft.EntityFrameworkCore;
using TodoService.Models;

namespace TodoService.Data
{
    public class AppDbContext : DbContext
    {
        // Pont entre notre model et notre BDD
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){}

        public DbSet<Todo> todo  { get; set; }
    }
}